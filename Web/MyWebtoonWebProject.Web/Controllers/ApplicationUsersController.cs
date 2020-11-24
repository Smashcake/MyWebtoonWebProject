namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using MyWebtoonWebProject.Services.Data;

    public class ApplicationUsersController : Controller
    {
        private readonly IAppicationUsersService appicationUsersService;

        public ApplicationUsersController(IAppicationUsersService appicationUsersService)
        {
            this.appicationUsersService = appicationUsersService;
        }

        public ActionResult<string> GetUserSubscribtions()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return "Hello from userSubscribtions";
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
