namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    public interface IWebtoonsSubscribersService
    {
        Task<bool> SubscribeUserToWebtoon(string webtoonId, string userId);
    }
}
