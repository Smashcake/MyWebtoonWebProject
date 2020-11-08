namespace MyWebtoonWebProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class WebtoonsController : BaseController
    {
        public IActionResult CreateWebtoon()
        {
            return this.View();
        }
    }
}
