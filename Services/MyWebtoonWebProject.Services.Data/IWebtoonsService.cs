namespace MyWebtoonWebProject.Services
{
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public interface IWebtoonsService
    {
        Task<int> CreateWebtoonAsync(CreateWebtoonInputModel input);
    }
}
