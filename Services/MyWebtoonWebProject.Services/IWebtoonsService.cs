namespace MyWebtoonWebProject.Services
{
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public interface IWebtoonsService
    {
        void CreateWebtoon(CreateWebtoonInputModel input);
    }
}
