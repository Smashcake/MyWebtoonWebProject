namespace MyWebtoonWebProject.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Repositories;

    public class WebtoonsSubscribersService : IWebtoonsSubscribersService
    {
        private readonly IWebtoonsSubscribersRepository webtoonsSubscribersRepository;

        public WebtoonsSubscribersService(IWebtoonsSubscribersRepository webtoonsSubscribersRepository)
        {
            this.webtoonsSubscribersRepository = webtoonsSubscribersRepository;
        }

        public async Task<bool> SubscribeUserToWebtoon(string webtoonId, string userId)
        {
            var subscription = this.webtoonsSubscribersRepository
                .All()
                .FirstOrDefault(ws => ws.SubscriberId == userId && ws.WebtoonId == webtoonId);

            if (subscription == null)
            {
                subscription = new WebtoonsSubscribers
                {
                    SubscriberId = userId,
                    WebtoonId = webtoonId,
                };

                await this.webtoonsSubscribersRepository.AddAsync(subscription);
                await this.webtoonsSubscribersRepository.SaveChangesAsync();
                return true;
            }
            else
            {
                this.webtoonsSubscribersRepository.Delete(subscription);
                await this.webtoonsSubscribersRepository.SaveChangesAsync();
                return false;
            }
        }
    }
}
