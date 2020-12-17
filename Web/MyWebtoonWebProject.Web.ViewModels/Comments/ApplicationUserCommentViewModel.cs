namespace MyWebtoonWebProject.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;

    using MyWebtoonWebProject.Data.Models;

    public class ApplicationUserCommentViewModel
    {
        public string CommentId { get; set; }

        public string WebtoonTitle { get; set; }

        public string WebtoonTitleNumber { get; set; }

        public string CommentAuthorId { get; set; }

        public string CommentNumber { get; set; }

        public string EpisodeNumber { get; set; }

        public string CommentInfo { get; set; }

        public ICollection<CommentVote> CommentVotes { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
