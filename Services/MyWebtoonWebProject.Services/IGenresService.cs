namespace MyWebtoonWebProject.Services
{
    using MyWebtoonWebProject.Web.ViewModels.Genres;

    public interface IGenresService
    {
        void CreateGenre(CreateGenreInputModel input);
    }
}
