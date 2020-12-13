namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels.WebtoonsRatings;

    [ApiController]
    [Route("api/[controller]")]
    public class WebtoonsRatingsController : ControllerBase
    {
        private readonly IWebtoonsRatingsService webtoonsRatingsService;

        public WebtoonsRatingsController(IWebtoonsRatingsService webtoonsRatingsService)
        {
            this.webtoonsRatingsService = webtoonsRatingsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<WebtoonRatingResponseModel>> RateWebtoon(WebtoonRatingInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.webtoonsRatingsService.RateWebtoonAsync(input.WebtoonTitleNumber, userId, (byte)input.RatingValue);
            var webtoonAverageRating = this.webtoonsRatingsService.GetWebtoonAverageRating(input.WebtoonTitleNumber);
            return new WebtoonRatingResponseModel { AverageWebtoonRating = webtoonAverageRating };
        }
    }
}
