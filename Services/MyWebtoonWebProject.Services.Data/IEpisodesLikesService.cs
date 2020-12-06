namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    public interface IEpisodesLikesService
    {
        int GetEpisodeLikes(string episodeId);

        Task UserLikeAsync(string episodeId, string userId);
    }
}
