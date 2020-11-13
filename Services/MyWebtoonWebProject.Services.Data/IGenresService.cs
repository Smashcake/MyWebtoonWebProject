namespace MyWebtoonWebProject.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Genres;

    public interface IGenresService
    {
        Task<int> CreateGenreAsync(CreateGenreInputModel input);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
