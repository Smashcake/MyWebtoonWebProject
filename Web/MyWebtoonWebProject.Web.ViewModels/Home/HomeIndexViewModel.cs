namespace MyWebtoonWebProject.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public class HomeIndexViewModel
    {
        public ICollection<MostPopularWebtoonsViewModel> MostPopular { get; set; }

        public ICollection<GetWebtoonInfoViewModel> DailyUploads { get; set; }
    }
}
