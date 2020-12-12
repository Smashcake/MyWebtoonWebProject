namespace MyWebtoonWebProject.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Genres;

    public interface IGenresService
    {
        Task CreateGenreAsync(CreateGenreInputModel input);

        Task DeleteGenre(string id);

        Task EditGenre(EditGenreInputModel input);

        string GetGenreName(string id);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        ICollection<GenreViewModel> AllGenres();
    }
}
