namespace MyWebtoonWebProject.Web.ViewModels.Webtoons
{
    using System.Collections.Generic;

    using MyWebtoonWebProject.Data.Models;

    public class WebtoonInfoViewModel
    {
        public string CoverPhoto { get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public ApplicationUser Author { get; set; }

        public string GenreName { get; set; }

        public string UploadDay { get; set; }

        public string Synopsis { get; set; }

        public ICollection<Episode> Episodes { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
