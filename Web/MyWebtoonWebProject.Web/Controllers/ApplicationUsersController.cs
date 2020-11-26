namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public class ApplicationUsersController : Controller
    {
        private readonly IAppicationUsersService appicationUsersService;

        public ApplicationUsersController(IAppicationUsersService appicationUsersService)
        {
            this.appicationUsersService = appicationUsersService;
        }

        public IActionResult GetUserSubscribtions(GetAllWebtoonsViewModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            input.Webtoons = this.appicationUsersService.GetUserSubscribtions(userId);
            return this.View(input);
        }

        public ActionResult<string> GetUserComments()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return "Hello from userComments";
        }

        public ActionResult<string> GetUserReviews()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return "Hello from userReviews";
        }
    }
}
