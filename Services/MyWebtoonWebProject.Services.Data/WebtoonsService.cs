namespace MyWebtoonWebProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Repositories;
    using MyWebtoonWebProject.Web.ViewModels.Episodes;
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public class WebtoonsService : IWebtoonsService
    {
        private readonly IWebtoonsRepository webtoonsRepository;
        private readonly IEpisodesRepository episodesRepository;
        private readonly IGenresRepository genresRepository;
        private readonly IApplicationUserRepository applicationUserRepository;

        public WebtoonsService(IWebtoonsRepository webtoonsRepository, IEpisodesRepository episodesRepository, IGenresRepository genresRepository, IApplicationUserRepository applicationUserRepository)
        {
            this.webtoonsRepository = webtoonsRepository;
            this.episodesRepository = episodesRepository;
            this.genresRepository = genresRepository;
            this.applicationUserRepository = applicationUserRepository;
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

            string coverPhotoPath = webtoonFolder + "/cover.png";
            using (FileStream fs = new FileStream(coverPhotoPath, FileMode.Create))
            {
                await input.Cover.CopyToAsync(fs);
            }

            var webtoon = new Webtoon
            {
                Title = input.Title,
                Synopsis = input.Synopsis,
                CoverPhoto = input.Title + "/cover.png",
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
                    Likes = w.Episodes.Sum(e => e.Likes),
                    TitleNumber = w.TitleNumber,
                })
                .ToList();

            return webtoons;
        }

        public WebtoonInfoViewModel GetWebtoon(string titleNumber)
        {
            var webtoon = this.webtoonsRepository
                .GetWebtoonByTitleNumber(titleNumber);

            webtoon.Episodes = this.episodesRepository.GetEpisodesByWebtoonId(webtoon.Id);
            webtoon.Genre = this.genresRepository.GetGenreByWebtoonGenreId(webtoon.GenreId);

            return new WebtoonInfoViewModel
            {
                AuthorName = this.applicationUserRepository.GetAuthorUsername(webtoon.AuthorId),
                AuthorId = webtoon.AuthorId,
                Episodes = webtoon.Episodes.Select(e => new EpisodeWebtoonViewModel
                {
                    EpisodeNumber = e.Name,
                    CreatedOn = e.CreatedOn,
                    Likes = e.Likes,
                }),
                Id = webtoon.Id,
                GenreName = webtoon.Genre.Name,
                Synopsis = webtoon.Synopsis,
                Title = webtoon.Title,
                CoverPhoto = webtoon.CoverPhoto,
                UploadDay = webtoon.UploadDay.ToString(),
                Reviews = webtoon.Reviews,
                TitleNumber = webtoon.TitleNumber,
            };
        }
    }
}
