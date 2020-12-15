namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using MyWebtoonWebProject.Services;
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public class WebtoonsController : Controller
    {
        private readonly IWebtoonsService webtoonsService;
        private readonly IGenresService genresService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public WebtoonsController(IWebtoonsService webtoonsService, IGenresService genresService, IWebHostEnvironment webHostEnvironment)
        {
            this.webtoonsService = webtoonsService;
            this.genresService = genresService;
            this.webHostEnvironment = webHostEnvironment;
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

            var webRootPath = this.webHostEnvironment.WebRootPath;

            input.AuthorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.webtoonsService.CreateWebtoonAsync(input, webRootPath);
            return this.Redirect("/");
        }

        public IActionResult GetAllWebtoons(GetAllWebtoonsViewModel input)
        {
            input.Webtoons = this.webtoonsService.GetAllWebtoons();
            return this.View(input);
        }

        public IActionResult GetWebtoon(string webtoonTitleNumber, int id = 1)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            const int episodesPerPage = 10;
            var input = this.webtoonsService.GetWebtoon(webtoonTitleNumber, id, episodesPerPage, currentUserId);
            input.PageNumber = id;
            input.EpisodesPerPage = episodesPerPage;
            return this.View(input);
        }

        [Authorize]
        public async Task<IActionResult> DeleteWebtoon(string webtoonTitleNumber)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.webtoonsService.DeleteWebtoon(webtoonTitleNumber, currentUserId);
            return this.RedirectToAction("GetAllWebtoons");
        }

        [Authorize]
        public IActionResult EditWebtoon(string webtoonTitleNumber)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var webtoon = this.webtoonsService.GetWebtoonToEdit(webtoonTitleNumber, currentUserId);
            webtoon.Genres = this.genresService.GetAllAsKeyValuePairs();
            return this.View(webtoon);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditWebtoon(EditWebtoonInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Genres = this.genresService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.webtoonsService.EditWebtoon(input, userId);
            return this.RedirectToAction("GetAllWebtoons");
        }
    }
}
