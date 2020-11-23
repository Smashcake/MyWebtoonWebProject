namespace MyWebtoonWebProject.Data.Repositories
{
    using MyWebtoonWebProject.Data.Common.Repositories;
    using MyWebtoonWebProject.Data.Models;

    public interface IWebtoonsSubscribersRepository : IRepository<WebtoonsSubscribers>
    {
        bool IsUserSubscribed(string webtoonId, string userId);
    }
}
