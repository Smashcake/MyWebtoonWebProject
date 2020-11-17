namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Episodes;

    public interface IEpisodesService
    {
        Task AddEpisodeAsync(AddEpisodeInputModel input);
    }
}
