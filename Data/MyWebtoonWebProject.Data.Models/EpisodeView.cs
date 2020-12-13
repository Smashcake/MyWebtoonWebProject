namespace MyWebtoonWebProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EpisodeView
    {
        [Required]
        public string EpisodeId { get; set; }

        public virtual Episode Episode { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime LastViewedOn { get; set; }

        public int ViewCount { get; set; }
    }
}
