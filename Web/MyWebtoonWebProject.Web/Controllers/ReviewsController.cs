namespace MyWebtoonWebProject.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewsController : Controller
    {
        [Authorize]
        public IActionResult LeaveReview(string id)
        {
            return this.View();
        }
    }
}
