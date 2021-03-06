﻿namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels.Episodes;

    public class EpisodesController : Controller
    {
        private readonly IEpisodesService episodesService;
        private readonly IEpisodesViewsService episodesViewsService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EpisodesController(IEpisodesService episodesService, IEpisodesViewsService episodesViewsService, IWebHostEnvironment webHostEnvironment)
        {
            this.episodesService = episodesService;
            this.episodesViewsService = episodesViewsService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public IActionResult AddEpisode(string webtoonTitleNumber)
        {
            var input = new AddEpisodeInputModel
            {
                TitleNumber = webtoonTitleNumber,
            };
            return this.View(input);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddEpisodeAsync(AddEpisodeInputModel input)
        {
            var webRootPath = this.webHostEnvironment.WebRootPath;
            await this.episodesService.AddEpisodeAsync(input, webRootPath);
            return this.Redirect("/");
        }

        public async Task<IActionResult> GetEpisode(string webtoonNumber, string episodeNumber)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                await this.episodesViewsService.UserEpisodeView(webtoonNumber, episodeNumber, userId);
            }

            var viewModel = this.episodesService.GetEpisode(webtoonNumber, episodeNumber);
            return this.View(viewModel);
        }

        public async Task<IActionResult> DeleteEpisodeAsync(string webtoonTitleNumber, string episodeNumber)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.episodesService.DeleteEpisodeAsync(webtoonTitleNumber, episodeNumber, userId);
            return this.RedirectToAction("GetWebtoon", "Webtoons", new { webtoonTitleNumber });
        }

        public IActionResult LatestEpisodes(LatestEpisodesViewModel input)
        {
            input.LatestEpisodes = this.episodesService.LatestEpisodes();
            return this.View(input);
        }
    }
}
