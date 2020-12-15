namespace MyWebtoonWebProject.Web.ViewModels.Webtoons
{
    using System.Collections.Generic;

    public class AllWebtoonsViewModel
    {
        public ICollection<GetAllWebtoonsByGenreViewModel> Genres { get; set; }
    }
}
