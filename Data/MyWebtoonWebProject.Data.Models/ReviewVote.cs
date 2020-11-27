namespace MyWebtoonWebProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyWebtoonWebProject.Data.Models.Enums;

    public class ReviewVote
    {
        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string ReviewId { get; set; }

        public virtual Review Review { get; set; }

        public VoteType Vote { get; set; }
    }
}
