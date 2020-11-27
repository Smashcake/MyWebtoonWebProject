namespace MyWebtoonWebProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyWebtoonWebProject.Data.Models.Enums;

    public class CommentVote
    {
        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public VoteType Vote { get; set; }
    }
}
