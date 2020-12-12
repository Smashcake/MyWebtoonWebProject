namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyWebtoonWebProject.Services;
    using MyWebtoonWebProject.Web.ViewModels.Genres;

    [Authorize(Roles = "Administrator")]
    public class GenresController : Controller
    {
        private readonly IGenresService genresService;

        public GenresController(IGenresService genresService)
        {
            this.genresService = genresService;
        }

        public IActionResult CreateGenre()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre(CreateGenreInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.genresService.CreateGenreAsync(input);
            return this.Redirect("/");
        }

        public IActionResult AllGenres(AllGenresViewModel input)
        {
            input.Genres = this.genresService.AllGenres();
            return this.View(input);
        }

        public async Task<IActionResult> DeleteGenre(string id)
        {
            await this.genresService.DeleteGenre(id);
            return this.RedirectToAction("AllGenres");
        }

        public IActionResult EditGenre(string id)
        {
            var genre = new EditGenreInputModel
            {
                Name = this.genresService.GetGenreName(id),
            };
            return this.View(genre);
        }

        [HttpPost]
        public async Task<IActionResult> EditGenre(EditGenreInputModel input)
        {
            await this.genresService.EditGenre(input);
            return this.RedirectToAction("AllGenres");
        }
    }
}
