namespace MyWebtoonWebProject.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public interface IWebtoonsService
    {
        Task CreateWebtoonAsync(CreateWebtoonInputModel input);

        Task DeleteWebtoon(string webtoonTitleNumber, string userId);

        ICollection<GetWebtoonInfoViewModel> GetAllWebtoons();

        ICollection<GetWebtoonInfoViewModel> GetDailyUploads(string currentDay);

        WebtoonInfoViewModel GetWebtoon(string titleNumber, int page, int episodesPerPage, string userId);
    }
}
