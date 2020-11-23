namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using MyWebtoonWebProject.Data.Repositories;
    using MyWebtoonWebProject.Services.Data;
    using MyWebtoonWebProject.Web.ViewModels;
    using MyWebtoonWebProject.Web.ViewModels.WebtoonsSubscribers;

    [ApiController]
    [Route("api/[controller]")]
    public class SubscribesController : BaseController
    {
        private readonly IWebtoonsRepository webtoonsRepository;
        private readonly IWebtoonsSubscribersService webtoonsSubscribersService;

        public SubscribesController(IWebtoonsRepository webtoonsRepository, IWebtoonsSubscribersService webtoonsSubscribersService)
        {
            this.webtoonsRepository = webtoonsRepository;
            this.webtoonsSubscribersService = webtoonsSubscribersService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<WebtoonSubscriberResponseModel>> Post(WebtoonsSubscribersInputModel input)
        {
            var webtoonId = this.webtoonsRepository.GetWebtoonByTitleNumber(input.WebtoonTitleNumber).Id;
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isUserSubscribed = await this.webtoonsSubscribersService.SubscribeUserToWebtoon(webtoonId, currentUserId);
            return new WebtoonSubscriberResponseModel { IsUserSubscribed = isUserSubscribed };
        }
    }
}
