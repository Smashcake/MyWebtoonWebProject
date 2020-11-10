namespace MyWebtoonWebProject.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyWebtoonWebProject.Services;
    using MyWebtoonWebProject.Web.ViewModels.Genres;

    public class GenresController : BaseController
    {
        private readonly IGenresService genresService;

        public GenresController(IGenresService genresService)
        {
            this.genresService = genresService;
        }

        [Authorize]
        public IActionResult CreateGenre()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateGenre(CreateGenreInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.genresService.CreateGenre(input);
            return this.Redirect("/");
        }
    }
}
