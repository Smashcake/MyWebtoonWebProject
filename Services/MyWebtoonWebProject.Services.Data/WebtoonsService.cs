namespace MyWebtoonWebProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Models.Enums;
    using MyWebtoonWebProject.Data.Repositories;
    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels.Episodes;
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public class WebtoonsService : IWebtoonsService
    {
        private readonly IWebtoonsRepository webtoonsRepository;
        private readonly IEpisodesRepository episodesRepository;
        private readonly IGenresRepository genresRepository;
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly IWebtoonsSubscribersRepository webtoonsSubscribersRepository;
        private readonly IReviewsRepository reviewsRepository;
        private readonly IReviewsVotesRepository reviewsVotesRepository;
        private readonly IEpisodesLikesService episodesLikesService;
        private readonly IWebtoonsRatingsService webtoonsRatingsService;
        private readonly IEpisodesViewsService episodesViewsService;

        public WebtoonsService(
            IWebtoonsRepository webtoonsRepository,
            IEpisodesRepository episodesRepository,
            IGenresRepository genresRepository,
            IApplicationUserRepository applicationUserRepository,
            IWebtoonsSubscribersRepository webtoonsSubscribersRepository,
            IReviewsRepository reviewsRepository,
            IReviewsVotesRepository reviewsVotesRepository,
            IEpisodesLikesService episodesLikesService,
            IWebtoonsRatingsService webtoonsRatingsService,
            IEpisodesViewsService episodesViewsService)
        {
            this.webtoonsRepository = webtoonsRepository;
            this.episodesRepository = episodesRepository;
            this.genresRepository = genresRepository;
            this.applicationUserRepository = applicationUserRepository;
            this.webtoonsSubscribersRepository = webtoonsSubscribersRepository;
            this.reviewsRepository = reviewsRepository;
            this.reviewsVotesRepository = reviewsVotesRepository;
            this.episodesLikesService = episodesLikesService;
            this.webtoonsRatingsService = webtoonsRatingsService;
            this.episodesViewsService = episodesViewsService;
        }

        public async Task CreateWebtoonAsync(CreateWebtoonInputModel input, string webRootPath)
        {
            if (this.webtoonsRepository.WebtoonExists(input.Title))
            {
                throw new ArgumentException("Webtoon already exists!");
            }

            string topFolder = $@"{webRootPath}\Webtoons";
            string webtoonFolder = Path.Combine(topFolder, input.Title);
            Directory.CreateDirectory(webtoonFolder);

            var extention = Path.GetExtension(input.Cover.FileName).TrimStart('.');

            string coverPhotoPath = webtoonFolder + $"/cover.{extention}";
            using (FileStream fs = new FileStream(coverPhotoPath, FileMode.Create))
            {
                await input.Cover.CopyToAsync(fs);
            }

            var webtoon = new Webtoon
            {
                Title = input.Title,
                Synopsis = input.Synopsis,
                CoverPhoto = input.Title + $"/cover.{extention}",
                GenreId = input.GenreId,
                UploadDay = input.UploadDay,
                AuthorId = input.AuthorId,
                CreatedOn = DateTime.UtcNow,
                Completed = false,
                TitleNumber = (this.webtoonsRepository.GetWebtoonsCount() + 1).ToString(),
            };

            await this.webtoonsRepository.AddAsync(webtoon);
            await this.webtoonsRepository.SaveChangesAsync();
        }

        public ICollection<GetAllWebtoonsByGenreViewModel> GetAllWebtoons()
        {
            var groupedWebtoons = this.webtoonsRepository.All()
                .ToList()
                .GroupBy(w => this.genresRepository.GetGenreByWebtoonGenreId(w.GenreId).Name)
                .ToList()
                .Select(w => new GetAllWebtoonsByGenreViewModel
                {
                    GenreName = w.Key,
                    Webtoons = w.Select(x => new GetWebtoonInfoViewModel
                    {
                        Author = this.applicationUserRepository.GetWebtoonAuthorUsername(x.AuthorId),
                        Title = x.Title,
                        CoverPhoto = x.CoverPhoto,
                        TitleNumber = x.TitleNumber,
                        Episodes = this.episodesRepository.GetEpisodesByWebtoonId(x.Id),
                        Likes = x.Episodes.Sum(e => this.episodesLikesService.GetEpisodeLikes(e.Id)),
                    }).ToList(),
                })
                .OrderBy(w => w.GenreName)
                .ToList();

            return groupedWebtoons;
        }

        public WebtoonInfoViewModel GetWebtoon(string webtoonTitleNumber, int page, int episodesPerPage, string userId)
        {
            var webtoon = this.webtoonsRepository
                .GetWebtoonByTitleNumber(webtoonTitleNumber);

            webtoon.Episodes = this.episodesRepository.GetEpisodesByWebtoonId(webtoon.Id);
            webtoon.Reviews = this.reviewsRepository.GetReviewsByWebtoonId(webtoon.Id);
            webtoon.Genre = this.genresRepository.GetGenreByWebtoonGenreId(webtoon.GenreId);

            foreach (var review in webtoon.Reviews)
            {
                review.ReviewVotes = this.reviewsVotesRepository.GetReviewVotes(review.Id);
            }

            return new WebtoonInfoViewModel
            {
                AuthorName = this.applicationUserRepository.GetWebtoonAuthorUsername(webtoon.AuthorId),
                AuthorId = webtoon.AuthorId,
                Episodes = webtoon.Episodes.Select(e => new EpisodeWebtoonViewModel
                {
                    EpisodeNumber = e.EpisodeNumber,
                    CreatedOn = e.CreatedOn,
                    Likes = this.episodesLikesService.GetEpisodeLikes(e.Id),
                    EpisodeName = e.Name,
                })
                .OrderByDescending(e => e.EpisodeNumber)
                .Skip((page - 1) * episodesPerPage)
                .Take(episodesPerPage),
                IsUserSubscribed = this.webtoonsSubscribersRepository.IsUserSubscribed(webtoon.Id, userId),
                EpisodesCount = webtoon.Episodes.Count,
                GenreName = webtoon.Genre.Name,
                Synopsis = webtoon.Synopsis,
                AverageRating = this.webtoonsRatingsService.DoesWebtoonHaveARating(webtoon.TitleNumber) ? this.webtoonsRatingsService.GetWebtoonAverageRating(webtoon.TitleNumber) : 0,
                Title = webtoon.Title,
                CoverPhoto = webtoon.CoverPhoto,
                UploadDay = webtoon.UploadDay.ToString(),
                DoesCurrentUserHaveAReview = webtoon.Reviews.Any(r => r.ReviewAuthorId == userId),
                Reviews = webtoon.Reviews
                .Select(r => new ReviewsWebtoonViewModel
                {
                    ReviewNumber = r.ReviewNumber,
                    ReviewInfo = r.ReviewInfo,
                    AuthorUsername = r.ReviewAuthor.UserName,
                    AuthorId = r.ReviewAuthorId,
                    CreatedOn = r.CreatedOn,
                    Likes = r.ReviewVotes.Sum(rv => rv.Vote.Equals(VoteType.UpVote) ? 1 : 0),
                    Dislikes = r.ReviewVotes.Sum(rv => rv.Vote.Equals(VoteType.DownVote) ? 1 : 0),
                }),
                TitleNumber = webtoon.TitleNumber,
            };
        }

        public ICollection<GetWebtoonInfoViewModel> GetDailyUploads(string currentDay)
        {
            var allWebtoons = this.webtoonsRepository.All().ToList();
            var dailyUploads = allWebtoons.Where(w => w.UploadDay.ToString() == currentDay).ToList();

            foreach (var webtoon in dailyUploads)
            {
                webtoon.Episodes = this.episodesRepository.GetEpisodesByWebtoonId(webtoon.Id);
            }

            var webtoons = dailyUploads.Select(w => new GetWebtoonInfoViewModel
            {
                Author = this.applicationUserRepository.GetWebtoonAuthorUsername(w.AuthorId),
                Title = w.Title,
                CoverPhoto = w.CoverPhoto,
                Genre = this.genresRepository.GetGenreByWebtoonGenreId(w.GenreId).Name,
                TitleNumber = w.TitleNumber,
                Episodes = w.Episodes,
                Likes = w.Episodes.Sum(e => this.episodesLikesService.GetEpisodeLikes(e.Id)),
            })
            .OrderBy(w => Guid.NewGuid())
            .Take(10)
            .ToList();

            return webtoons;
        }

        public ICollection<MostPopularWebtoonsViewModel> MostPopular()
        {
            var allWebtoons = this.webtoonsRepository.All().ToList();

            foreach (var webtoon in allWebtoons)
            {
                webtoon.Episodes = this.episodesRepository.GetEpisodesByWebtoonId(webtoon.Id);
            }

            var webtoons = allWebtoons.Select(w => new MostPopularWebtoonsViewModel
            {
                Title = w.Title,
                CoverPhoto = w.CoverPhoto,
                Genre = this.genresRepository.GetGenreByWebtoonGenreId(w.GenreId).Name,
                TitleNumber = w.TitleNumber,
                Synopsis = w.Synopsis.Substring(0, w.Synopsis.Length > 100 ? 100 : w.Synopsis.Length) + "...",
                Episodes = w.Episodes,
                TotalViews = w.Episodes.Sum(e => this.episodesViewsService.EpisodeTotalViews(w.TitleNumber, e.EpisodeNumber)),
                Likes = w.Episodes.Sum(e => this.episodesLikesService.GetEpisodeLikes(e.Id)),
            })
            .OrderBy(w => w.TotalViews)
            .ThenBy(w => w.Likes)
            .Take(5)
            .ToList();

            return webtoons;
        }

        public async Task DeleteWebtoonAsync(string webtoonTitleNumber, string userId)
        {
            var webtoon = this.webtoonsRepository.GetWebtoonByTitleNumber(webtoonTitleNumber);

            if (webtoon.AuthorId != userId)
            {
                throw new ArgumentException("Invalid data!");
            }

            this.webtoonsRepository.Delete(webtoon);
            await this.webtoonsRepository.SaveChangesAsync();
        }

        public EditWebtoonInputModel GetWebtoonToEdit(string webtoonTitleNumber, string userId)
        {
            var webtoon = this.webtoonsRepository.GetWebtoonByTitleNumber(webtoonTitleNumber);

            if (webtoon.AuthorId != userId)
            {
                throw new ArgumentException("Invalid data!");
            }

            var info = new EditWebtoonInputModel
            {
                OldTitle = webtoon.Title,
                Title = webtoon.Title,
                Synopsis = webtoon.Synopsis,
                UploadDay = webtoon.UploadDay,
                WebtoonTitleNumber = webtoon.TitleNumber,
            };

            return info;
        }

        public async Task EditWebtoon(EditWebtoonInputModel input, string userId, string webRootPath)
        {
            var webtoon = this.webtoonsRepository.GetWebtoonByTitleNumber(input.WebtoonTitleNumber);

            if (webtoon.AuthorId != userId)
            {
                throw new ArgumentException("Invalid operation.");
            }

            if (input.Title != input.OldTitle)
            {
                var topFolder = $@"{webRootPath}\Webtoons";
                var webtoonFolder = Path.Combine(topFolder, input.OldTitle);
                var newWebtoonFolder = Path.Combine(topFolder, input.Title);
                Directory.Move(webtoonFolder, newWebtoonFolder);
            }

            var extention = Path.GetExtension(webtoon.CoverPhoto).TrimStart('.');

            webtoon.GenreId = input.GenreId;
            webtoon.UploadDay = input.UploadDay;
            webtoon.Title = input.Title;
            webtoon.Synopsis = input.Synopsis;
            webtoon.CoverPhoto = input.Title + $"/cover.{extention}";

            await this.webtoonsRepository.SaveChangesAsync();
        }
    }
}
