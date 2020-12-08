namespace MyWebtoonWebProject.Web.ViewModels.Comments
{
    using System;

    public class CommentReplyViewModel
    {
        public string ParentId { get; set; }

        public string CommentAuthorUsername { get; set; }

        public string CommentAuthorId { get; set; }

        public string CommentInfo { get; set; }

        public string CommentNumber { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
