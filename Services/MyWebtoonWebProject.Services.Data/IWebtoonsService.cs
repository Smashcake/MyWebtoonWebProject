namespace MyWebtoonWebProject.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public interface IWebtoonsService
    {
        Task CreateWebtoonAsync(CreateWebtoonInputModel input);

        ICollection<GetWebtoonInfoViewModel> GetAllWebtoons();

        WebtoonInfoViewModel GetWebtoon(string titleNumber);
    }
}
