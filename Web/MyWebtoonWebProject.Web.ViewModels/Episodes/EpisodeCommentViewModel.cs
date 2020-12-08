namespace MyWebtoonWebProject.Web.ViewModels.Episodes
{
    using System;
    using System.Collections.Generic;

    using MyWebtoonWebProject.Web.ViewModels.Comments;

    public class EpisodeCommentViewModel
    {
        public string ParentId { get; set; }

        public string Id { get; set; }

        public string CommentAuthorUsername { get; set; }

        public string CommentNumber { get; set; }

        public string CommentAuthorId { get; set; }

        public string CommentInfo { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<CommentReplyViewModel> CommentReplies { get; set; }
    }
}
