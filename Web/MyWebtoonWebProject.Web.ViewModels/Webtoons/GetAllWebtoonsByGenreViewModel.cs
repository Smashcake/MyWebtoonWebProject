namespace MyWebtoonWebProject.Web.ViewModels.Webtoons
{
    using System.Collections.Generic;

    public class GetAllWebtoonsByGenreViewModel
    {
        public string GenreName { get; set; }

        public ICollection<GetWebtoonInfoViewModel> Webtoons { get; set; }
    }
}
