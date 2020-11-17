﻿namespace MyWebtoonWebProject.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

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
            var webtoon = this.dbContext.Webtoons.FirstOrDefault(w => w.Id == input.WebtoonId);
            var episodesCount = webtoon.Episodes.Count + 1;
            string episodeName = "Episode" + episodesCount;
            string topFolder = $@"C:\MyWebtoonWebProject\MyWebtoonWebProject\Web\MyWebtoonWebProject.Web\wwwroot\Webtoons\{webtoon.Title}";
            string episodeFolder = Path.Combine(topFolder, episodeName);
            Directory.CreateDirectory(episodeFolder);

            var episode = new Episode
            {
                Name = episodeName,
                Views = 0,
                WebtoonId = input.WebtoonId,
                IsDeleted = false,
                Likes = 0,
                UploadedOn = DateTime.UtcNow,
            };

            episode.Pages = this.pagesService.AddPagesAsync(input.Pages, episodeFolder, episode.Id).Result;

            await this.dbContext.Episodes.AddAsync(episode);
            await this.dbContext.SaveChangesAsync();
        }
    }
}