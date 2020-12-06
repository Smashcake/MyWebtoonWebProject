namespace MyWebtoonWebProject.Web.Controllers
{
    using System;
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using MyWebtoonWebProject.Services;
    using MyWebtoonWebProject.Web.ViewModels;
    using MyWebtoonWebProject.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IWebtoonsService webtoonsService;

        public HomeController(IWebtoonsService webtoonsService)
        {
            this.webtoonsService = webtoonsService;
        }

        public IActionResult Index(HomeIndexViewModel input)
        {
            input.DailyUploads = this.webtoonsService.GetDailyUploads(DateTime.UtcNow.DayOfWeek.ToString());
            return this.View(input);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
