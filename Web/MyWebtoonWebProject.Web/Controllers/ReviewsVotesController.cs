namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels.Reviews;

    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsVotesController : BaseController
    {
        private readonly IReviewsVotesService reviewsVotesService;
        private readonly IReviewsService reviewsService;

        public ReviewsVotesController(IReviewsVotesService reviewsVotesService, IReviewsService reviewsService)
        {
            this.reviewsVotesService = reviewsVotesService;
            this.reviewsService = reviewsService;
        }

        [HttpPost]
        public async Task<ActionResult<ReviewVoteResponseModel>> Vote(ReviewVoteInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.reviewsVotesService.UserVote(input.ReviewId, input.IsUpVote, userId);
            var likesAndDislikes = this.reviewsService.ReviewLikesAndDislikes(input.ReviewId);
            return new ReviewVoteResponseModel { Likes = likesAndDislikes.Likes, Dislikes = likesAndDislikes.Dislikes };
        }
    }
}
