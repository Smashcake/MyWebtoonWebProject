﻿namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyWebtoonWebProject.Services;
    using MyWebtoonWebProject.Web.ViewModels.Genres;

    public class GenresController : Controller
    {
        private readonly IGenresService genresService;

        public GenresController(IGenresService genresService)
        {
            this.genresService = genresService;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult CreateGenre()
        {
            return this.View();
        }

        [Authorize(Roles = "Administrator")]
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
    }
}
