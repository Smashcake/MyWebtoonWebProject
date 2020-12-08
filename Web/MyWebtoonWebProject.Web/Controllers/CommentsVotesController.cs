namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels.Comments;

    [ApiController]
    [Route("api/[controller]")]
    public class CommentsVotesController : BaseController
    {
        private readonly ICommentsVotesService commentsVotesService;

        public CommentsVotesController(ICommentsVotesService commentsVotesService)
        {
            this.commentsVotesService = commentsVotesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CommentVoteResponseModel>> Vote(CommentVoteInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.commentsVotesService.UserCommentVoteAsync(input.CommentNumber, input.IsUpVote, userId);
            var commentLikesAndDislikes = this.commentsVotesService.GetCommentVotes(input.CommentNumber);
            return commentLikesAndDislikes;
        }
    }
}
