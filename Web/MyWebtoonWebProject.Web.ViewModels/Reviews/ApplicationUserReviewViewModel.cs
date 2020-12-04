namespace MyWebtoonWebProject.Web.ViewModels.Reviews
{
    using System;

    public class ApplicationUserReviewViewModel
    {
        public string ReviewInfo { get; set; }

        public string ReviewNumber { get; set; }

        public string ReviewAuthorId { get; set; }

        public string WebtoonTitle { get; set; }

        public string WebtoonTitleNumber { get; set; }

        public string CoverPhoto { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
