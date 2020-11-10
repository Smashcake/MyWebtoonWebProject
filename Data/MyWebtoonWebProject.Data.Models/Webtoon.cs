namespace MyWebtoonWebProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyWebtoonWebProject.Data.Common.Models;

    public class Webtoon : BaseDeletableModel<string>
    {
        public Webtoon()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Subscribers = new HashSet<WebtoonsSubscribers>();
            this.Episodes = new HashSet<Episode>();
            this.Reviews = new HashSet<Review>();
        }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(600)]
        public string Synopsis { get; set; }

        [Required]
        public string CoverPhoto { get; set; }

        public decimal Rating { get; set; }

        public virtual ICollection<WebtoonsSubscribers> Subscribers { get; set; }

        public string GenreId { get; set; }

        public Genre Genre { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public Enums.DayOfWeek UploadDay { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public bool Completed { get; set; }
    }
}
