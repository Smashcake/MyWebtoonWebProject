namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyWebtoonWebProject.Data;
    using MyWebtoonWebProject.Services;
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public class WebtoonsController : BaseController
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebtoonsService webtoonsService;

        public WebtoonsController(ApplicationDbContext dbContext, IWebtoonsService webtoonsService)
        {
            this.dbContext = dbContext;
            this.webtoonsService = webtoonsService;
        }

        [Authorize]
        public IActionResult CreateWebtoon()
        {
            this.ViewBag.Genres = new SelectList(this.dbContext.Genres, "Id", "Name");
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateWebtoon(CreateWebtoonInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            input.AuthorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.webtoonsService.CreateWebtoon(input);
            return this.Redirect("/");
        }
    }
}
