namespace MyWebtoonWebProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EpisodeLike
    {
        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string EpisodeId { get; set; }

        public virtual Episode Episode { get; set; }

        public bool HasLiked { get; set; }
    }
}
