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

        public WebtoonsService(
            IWebtoonsRepository webtoonsRepository,
            IEpisodesRepository episodesRepository,
            IGenresRepository genresRepository,
            IApplicationUserRepository applicationUserRepository,
            IWebtoonsSubscribersRepository webtoonsSubscribersRepository,
            IReviewsRepository reviewsRepository,
            IReviewsVotesRepository reviewsVotesRepository,
            IEpisodesLikesService episodesLikesService)
        {
            this.webtoonsRepository = webtoonsRepository;
            this.episodesRepository = episodesRepository;
            this.genresRepository = genresRepository;
            this.applicationUserRepository = applicationUserRepository;
            this.webtoonsSubscribersRepository = webtoonsSubscribersRepository;
            this.reviewsRepository = reviewsRepository;
            this.reviewsVotesRepository = reviewsVotesRepository;
            this.episodesLikesService = episodesLikesService;
        }

        public async Task CreateWebtoonAsync(CreateWebtoonInputModel input)
        {
            if (this.webtoonsRepository.WebtoonExists(input.Title))
            {
                throw new ArgumentException("Webtoon already exists!");
            }

            string topFolder = @"C:\MyWebtoonWebProject\MyWebtoonWebProject\Web\MyWebtoonWebProject.Web\wwwroot\Webtoons";
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
                Rating = 0M,
                Completed = false,
                TitleNumber = (this.webtoonsRepository.GetWebtoonsCount() + 1).ToString(),
            };

            await this.webtoonsRepository.AddAsync(webtoon);
            await this.webtoonsRepository.SaveChangesAsync();
        }

        public ICollection<GetWebtoonInfoViewModel> GetAllWebtoons()
        {
            var webtoons = this.webtoonsRepository
                .AllAsNoTracking()
                .Select(w => new GetWebtoonInfoViewModel
                {
                    Author = w.Author.UserName,
                    Title = w.Title,
                    CoverPhoto = w.CoverPhoto,
                    Genre = w.Genre.Name,
                    TitleNumber = w.TitleNumber,
                })
                .ToList();

            return webtoons;
        }

        public WebtoonInfoViewModel GetWebtoon(string titleNumber, int page, int episodesPerPage, string userId)
        {
            var webtoon = this.webtoonsRepository
                .GetWebtoonByTitleNumber(titleNumber);

            webtoon.Episodes = this.episodesRepository.GetEpisodesByWebtoonId(webtoon.Id);
            webtoon.Reviews = this.reviewsRepository.GetReviewsByWebtoonId(webtoon.Id);
            webtoon.Genre = this.genresRepository.GetGenreByWebtoonGenreId(webtoon.GenreId);

            foreach (var review in webtoon.Reviews)
            {
                review.ReviewVotes = this.reviewsVotesRepository.GetReviewVotes(review.Id);
            }

            return new WebtoonInfoViewModel
            {
                AuthorName = this.applicationUserRepository.GetAuthorUsername(webtoon.AuthorId),
                AuthorId = webtoon.AuthorId,
                Episodes = webtoon.Episodes.Select(e => new EpisodeWebtoonViewModel
                {
                    EpisodeNumber = e.Name,
                    CreatedOn = e.CreatedOn,
                    Likes = this.episodesLikesService.GetEpisodeLikes(e.Id),
                })
                .OrderByDescending(e => e.EpisodeNumber)
                .Skip((page - 1) * episodesPerPage)
                .Take(episodesPerPage),
                IsUserSubscribed = this.webtoonsSubscribersRepository.IsUserSubscribed(webtoon.Id, userId),
                EpisodesCount = webtoon.Episodes.Count,
                GenreName = webtoon.Genre.Name,
                Synopsis = webtoon.Synopsis,
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
                    CreatedOn = r.CreatedOn,
                    Likes = r.ReviewVotes.Sum(rv => rv.Vote.Equals(VoteType.UpVote) ? 1 : 0),
                    Dislikes = r.ReviewVotes.Sum(rv => rv.Vote.Equals(VoteType.DownVote) ? 1 : 0),
                }),
                TitleNumber = webtoon.TitleNumber,
            };
        }
    }
}
