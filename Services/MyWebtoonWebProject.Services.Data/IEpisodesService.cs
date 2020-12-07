namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Episodes;

    public interface IEpisodesService
    {
        Task AddEpisodeAsync(AddEpisodeInputModel input);

        Task DeleteEpisodeAsync(string webtoonTitleNumber, string episodeNumber, string userId);

        GetEpisodeViewModel GetEpisode(string webtoonTitleNumber, string episodeNumber);

        string GetEpisodeId(string webtoonTitleNumber, string episodeNumber);
    }
}
