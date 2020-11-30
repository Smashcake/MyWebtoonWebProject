namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    public interface IEpisodesLikesService
    {
        int GetEpisodeLikes(string episodeId);

        Task UserLike(string episodeId, string userId);
    }
}
