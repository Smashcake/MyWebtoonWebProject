namespace MyWebtoonWebProject.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using MyWebtoonWebProject.Data;
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

        public void AddEpisode(AddEpisodeInputModel input)
        {
            var episodesCount = this.dbContext.Webtoons.FirstOrDefault(w => w.Id == input.WebtoonId).Episodes.Count + 1;
            string episodeName = "Episode" + episodesCount;
            string topFolder = @"C:\MyWebtoonWebProject\MyWebtoonWebProject\Web\MyWebtoonWebProject.Web\wwwroot\Webtoons";
            string episodeFolder = Path.Combine(topFolder, episodeName);
            Directory.CreateDirectory(episodeFolder);
        }
    }
}
