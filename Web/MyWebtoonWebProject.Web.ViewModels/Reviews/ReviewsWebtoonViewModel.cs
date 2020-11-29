namespace MyWebtoonWebProject.Web.ViewModels.Episodes
{
    using System;

    public class ReviewsWebtoonViewModel
    {
        public string ReviewNumber { get; set; }

        public string ReviewInfo { get; set; }

        public string AuthorUsername { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
