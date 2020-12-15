﻿namespace MyWebtoonWebProject.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public interface IWebtoonsService
    {
        Task CreateWebtoonAsync(CreateWebtoonInputModel input, string webRootPath);

        Task DeleteWebtoon(string webtoonTitleNumber, string userId);

        Task EditWebtoon(EditWebtoonInputModel input, string userId);

        ICollection<GetWebtoonInfoViewModel> GetAllWebtoons();

        ICollection<GetWebtoonInfoViewModel> GetDailyUploads(string currentDay);

        ICollection<MostPopularWebtoonsViewModel> MostPopular();

        WebtoonInfoViewModel GetWebtoon(string titleNumber, int page, int episodesPerPage, string userId);

        EditWebtoonInputModel GetWebtoonToEdit(string webtoonTitleNumber, string userId);
    }
}
