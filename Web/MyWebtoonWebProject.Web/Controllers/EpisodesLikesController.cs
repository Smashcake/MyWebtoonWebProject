namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels.Episodes;

    [ApiController]
    [Route("api/[controller]")]
    public class EpisodesLikesController : BaseController
    {
        private readonly IEpisodesService episodesService;
        private readonly IEpisodesLikesService episodesLikesService;

        public EpisodesLikesController(IEpisodesService episodesService, IEpisodesLikesService episodesLikesService)
        {
            this.episodesService = episodesService;
            this.episodesLikesService = episodesLikesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> LikeEpisode(EpisodeLikeInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var episodeId = this.episodesService.GetEpisodeId(input.WebtoonTitleNumber, input.EpisodeNumber);
            await this.episodesLikesService.UserLike(episodeId, userId);
            return this.episodesLikesService.GetEpisodeLikes(episodeId);
        }
    }
}
