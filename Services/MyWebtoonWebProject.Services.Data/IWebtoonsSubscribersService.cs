namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    public interface IWebtoonsSubscribersService
    {
        Task<bool> SubscribeUserToWebtoonAsync(string webtoonId, string userId);
    }
}
