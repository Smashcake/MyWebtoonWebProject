namespace MyWebtoonWebProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyWebtoonWebProject.Data.Common.Models;

    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<Comment>();
            this.CommentVotes = new HashSet<CommentVote>();
        }

        public string CommentAuthorId { get; set; }

        public virtual ApplicationUser CommentAuthor { get; set; }

        public string EpisodeId { get; set; }

        public virtual Episode Episode { get; set; }

        public virtual ICollection<CommentVote> CommentVotes { get; set; }

        public DateTime PostedOn { get; set; }

        [Required]
        [MaxLength(300)]
        public string CommentInfo { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public string CommentNumber { get; set; }

        public string ParentId { get; set; }
    }
}
