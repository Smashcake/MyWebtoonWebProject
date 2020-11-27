﻿namespace MyWebtoonWebProject.Web.ViewModels.Reviews
{
    using System;

    public class ApplicationUserReviewViewModel
    {
        public string ReviewInfo { get; set; }

        public string WebtoonTitle { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}