namespace MyWebtoonWebProject.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Repositories;
    using MyWebtoonWebProject.Web.ViewModels.Episodes;

    public class EpisodesService : IEpisodesService
    {
        private readonly IWebtoonsRepository webtoonsRepository;
        private readonly IEpisodesRepository episodesRepository;
        private readonly IPagesRepository pagesRepository;
        private readonly IPagesService pagesService;

        public EpisodesService(IWebtoonsRepository webtoonsRepository, IEpisodesRepository episodesRepository,IPagesRepository pagesRepository, IPagesService pagesService)
        {
            this.webtoonsRepository = webtoonsRepository;
            this.episodesRepository = episodesRepository;
            this.pagesRepository = pagesRepository;
            this.pagesService = pagesService;
        }

        public async Task AddEpisodeAsync(AddEpisodeInputModel input)
        {
            var webtoon = this.webtoonsRepository.GetWebtoonByTitleNumber(input.TitleNumber);
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

            await this.episodesRepository.AddAsync(episode);
            await this.episodesRepository.SaveChangesAsync();
        }

        public GetEpisodeViewModel GetEpisode(string webtoonTitleNumber, string episodeNumber)
        {
            var viewModel = new GetEpisodeViewModel();
            var episode = this.episodesRepository.GetEpisodeByWebtoonTitleNumber(webtoonTitleNumber, episodeNumber);
            viewModel.EpisodeNumber = episode.Name;
            viewModel.WebtoonTitle = this.webtoonsRepository.GetWebtoonByTitleNumber(webtoonTitleNumber).Title;
            viewModel.PagesPaths = this.pagesRepository.GetPagePathsForEpisodeByEpisodeId(episode.Id);
            return viewModel;
        }
    }
}
