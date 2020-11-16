namespace MyWebtoonWebProject.Services.Data
{
    using MyWebtoonWebProject.Web.ViewModels.Episodes;

    public interface IEpisodesService
    {
        void AddEpisode(AddEpisodeInputModel input);
    }
}
