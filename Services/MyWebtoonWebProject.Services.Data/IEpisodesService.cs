namespace MyWebtoonWebProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Episodes;

    public interface IEpisodesService
    {
        Task AddEpisodeAsync(AddEpisodeInputModel input, string webRootPath);

        Task DeleteEpisodeAsync(string webtoonTitleNumber, string episodeNumber, string userId);

        GetEpisodeViewModel GetEpisode(string webtoonTitleNumber, string episodeNumber);

        string GetEpisodeId(string webtoonTitleNumber, string episodeNumber);

        ICollection<LatestEpisodeViewModel> LatestEpisodes();
    }
}
