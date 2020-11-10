namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyWebtoonWebProject.Services;
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public class WebtoonsController : BaseController
    {
        private readonly IWebtoonsService webtoonsService;

        public WebtoonsController(IWebtoonsService webtoonsService)
        {
            this.webtoonsService = webtoonsService;
        }

        [Authorize]
        public IActionResult CreateWebtoon()
        {
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
