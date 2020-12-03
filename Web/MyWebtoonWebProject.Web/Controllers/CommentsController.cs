namespace MyWebtoonWebProject.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels.Comments;

    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment(CommentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                throw new ArgumentException("Invalid data");
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.commentsService.CreateComment(input, userId);
            return this.RedirectToAction($"GetEpisode", "Episodes", new { webtoonTitleNumber = input.WebtoonTitleNumber, episodeNumber = input.EpisodeNumber });
        }
    }
}
