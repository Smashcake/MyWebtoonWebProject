namespace MyWebtoonWebProject.Web.ViewModels.Webtoons
{
    using System.Collections.Generic;

    using MyWebtoonWebProject.Data.Models;

    public class GetWebtoonInfoViewModel
    {
        public string CoverPhoto { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public ICollection<Episode> Episodes { get; set; }

        public int Likes { get; set; }

        public string TitleNumber { get; set; }
    }
}
