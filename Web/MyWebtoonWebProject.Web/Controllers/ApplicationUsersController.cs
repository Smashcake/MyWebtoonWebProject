namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels.Reviews;
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

        public IActionResult GetUserReviews(ApplicationUserReviewsViewModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            input.Reviews = this.appicationUsersService.GetUserReviews(userId);
            return this.View(input);
        }
    }
}
