namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Linq;

    using MyWebtoonWebProject.Data.Models;

    public class WebtoonsSubscribersRepository : EfRepository<WebtoonsSubscribers>, IWebtoonsSubscribersRepository
    {
        public WebtoonsSubscribersRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public bool IsUserSubscribed(string webtoonId, string userId)
        {
            return this.DbSet.Any(ws => ws.SubscriberId == userId && ws.WebtoonId == webtoonId);
        }
    }
}
