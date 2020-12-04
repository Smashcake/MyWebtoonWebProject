﻿namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels.Reviews;

    [ApiController]
    [Route("api/Reviews")]
    public class ReviewsController : BaseController
    {
        private readonly IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<bool>> LeaveReview(LeaveReviewInputModel input)
        {
            input.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.reviewsService.AddReview(input);
            return true;
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteReview(DeleteReviewInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.reviewsService.DeleteReview(input.ReviewNumber, userId);
            return true;
        }
    }
}
