namespace MyWebtoonWebProject.Web.Controllers
{
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
        public IActionResult AddEpisode(string titleNumber)
        {
            var input = new AddEpisodeInputModel
            {
                TitleNumber = titleNumber,
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
    }
}
