namespace MyWebtoonWebProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels.Episodes;
    using System.Threading.Tasks;

    public class EpisodesController : Controller
    {
        private readonly IEpisodesService episodesService;

        public EpisodesController(IEpisodesService episodesService)
        {
            this.episodesService = episodesService;
        }

        public IActionResult AddEpisode(string id)
        {
            var input = new AddEpisodeInputModel
            {
                WebtoonId = id,
            };
            return this.View(input);
        }

        [HttpPost]
        public async Task<IActionResult> AddEpisode(AddEpisodeInputModel input)
        {
            await this.episodesService.AddEpisodeAsync(input);
            return this.Redirect($"/Webtoons/GetWebtoon?id={input.WebtoonId}");
        }
    }
}
