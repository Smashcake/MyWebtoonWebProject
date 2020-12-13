namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    public interface IEpisodesViewsService
    {
        Task UserEpisodeView(string webtoonTitleNumber, string episodeNumber, string userId);
    }
}
