namespace MyWebtoonWebProject.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Repositories;

    public class EpisodesViewsService : IEpisodesViewsService
    {
        private readonly IEpisodesViewsRepository episodesViewsRepository;
        private readonly IEpisodesService episodesService;

        public EpisodesViewsService(IEpisodesViewsRepository episodesViewsRepository, IEpisodesService episodesService)
        {
            this.episodesViewsRepository = episodesViewsRepository;
            this.episodesService = episodesService;
        }

        public async Task UserEpisodeView(string webtoonTitleNumber, string episodeNumber, string userId)
        {
            var episodeId = this.episodesService.GetEpisodeId(webtoonTitleNumber, episodeNumber);

            var userEpisodeView = this.episodesViewsRepository.All().FirstOrDefault(ev => ev.ApplicationUserId == userId & ev.EpisodeId == episodeId);

            if (userEpisodeView == null)
            {
                userEpisodeView = new EpisodeView
                {
                    ApplicationUserId = userId,
                    EpisodeId = episodeId,
                    LastViewedOn = DateTime.UtcNow,
                    ViewCount = 1,
                };

                await this.episodesViewsRepository.AddAsync(userEpisodeView);
            }
            else
            {
                var dateTimeDiff = DateTime.UtcNow.Subtract(userEpisodeView.LastViewedOn);
                if (dateTimeDiff.TotalHours > 24)
                {
                    userEpisodeView.ViewCount++;
                    userEpisodeView.LastViewedOn = DateTime.UtcNow;
                }
            }

            await this.episodesViewsRepository.SaveChangesAsync();
        }

        public double EpisodeTotalViews(string webtoonTitleNumber, string episodeNumber)
        {
            var episodeId = this.episodesService.GetEpisodeId(webtoonTitleNumber, episodeNumber);
            return this.episodesViewsRepository.All().Where(ev => ev.EpisodeId == episodeId).Sum(ev => ev.ViewCount);
    }
    }
}
