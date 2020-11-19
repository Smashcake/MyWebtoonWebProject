namespace MyWebtoonWebProject.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyWebtoonWebProject.Data;
    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Web.ViewModels.Episodes;

    public class EpisodesService : IEpisodesService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPagesService pagesService;

        public EpisodesService(ApplicationDbContext dbContext, IPagesService pagesService)
        {
            this.dbContext = dbContext;
            this.pagesService = pagesService;
        }

        public async Task AddEpisodeAsync(AddEpisodeInputModel input)
        {
            var webtoon = this.dbContext.Webtoons.FirstOrDefault(w => w.TitleNumber == input.TitleNumber);
            var episodesCount = webtoon.Episodes.Count + 1;
            string episodeName = "Episode" + episodesCount;
            string topFolder = $@"C:\MyWebtoonWebProject\MyWebtoonWebProject\Web\MyWebtoonWebProject.Web\wwwroot\Webtoons\{webtoon.Title}";
            string episodeFolder = Path.Combine(topFolder, episodeName);
            Directory.CreateDirectory(episodeFolder);

            var episode = new Episode
            {
                Name = episodeName,
                Views = 0,
                WebtoonId = webtoon.Id,
                IsDeleted = false,
                Likes = 0,
                CreatedOn = DateTime.UtcNow,
            };

            episode.Pages = this.pagesService.AddPagesAsync(input.Pages, episodeFolder, episode.Id).Result;

            await this.dbContext.Episodes.AddAsync(episode);
            await this.dbContext.SaveChangesAsync();
        }

        private bool IsImageValid(object value)
        {
            if (value is IFormFile file)
            {
                if (!(file.FileName.EndsWith(".png") || file.FileName.EndsWith(".jpg") || file.FileName.EndsWith(".jpeg")))
                {
                    return false;
                }

                if (file.Length > 10 * 1024 * 1024)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
