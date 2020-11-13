﻿namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyWebtoonWebProject.Services;
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public class WebtoonsController : BaseController
    {
        private readonly IWebtoonsService webtoonsService;
        private readonly IGenresService genresService;

        public WebtoonsController(IWebtoonsService webtoonsService, IGenresService genresService)
        {
            this.webtoonsService = webtoonsService;
            this.genresService = genresService;
        }

        [Authorize]
        public IActionResult CreateWebtoon()
        {
            var viewModel = new CreateWebtoonInputModel();
            viewModel.Genres = this.genresService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateWebtoon(CreateWebtoonInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Genres = this.genresService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            input.AuthorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.webtoonsService.CreateWebtoonAsync(input);
            return this.Redirect("/");
        }

        public IActionResult GetAllWebtoons(GetAllWebtoonsViewModel input)
        {

            return this.View(input);
        }
    }
}
