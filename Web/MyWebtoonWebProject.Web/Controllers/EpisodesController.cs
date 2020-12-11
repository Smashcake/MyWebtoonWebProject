namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels.Episodes;

    public class EpisodesController : Controller
    {
        private readonly IEpisodesService episodesService;

        public EpisodesController(IEpisodesService episodesService)
        {
            this.episodesService = episodesService;
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
            await this.episodesService.AddEpisodeAsync(input);
            return this.Redirect("/");
        }

        public IActionResult GetEpisode(string webtoonNumber, string episodeNumber)
        {
            var viewModel = this.episodesService.GetEpisode(webtoonNumber, episodeNumber);
            return this.View(viewModel);
        }

        public async Task<IActionResult> DeleteEpisodeAsync(string webtoonTitleNumber, string episodeNumber)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.episodesService.DeleteEpisodeAsync(webtoonTitleNumber, episodeNumber, userId);
            return this.RedirectToAction("GetWebtoon", "Webtoons", new { webtoonTitleNumber });
        }
    }
}
