namespace MyWebtoonWebProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ReviewsController : Controller
    {
        public IActionResult LeaveReview(string id)
        {
            return this.View();
        }
    }
}
