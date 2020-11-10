namespace MyWebtoonWebProject.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Web.ViewModels.Genres;

    public interface IGenresService
    {
        Task<int> CreateGenre(CreateGenreInputModel input);
    }
}
