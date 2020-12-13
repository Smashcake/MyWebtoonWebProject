namespace MyWebtoonWebProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyWebtoonWebProject.Data.Common.Models;

    public class Episode : BaseDeletableModel<string>
    {
        public Episode()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Pages = new HashSet<Page>();
            this.Comments = new HashSet<Comment>();
            this.EpisodeLikes = new HashSet<EpisodeLike>();
            this.EpisodeViews = new HashSet<EpisodeViews>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string EpisodeNumber { get; set; }

        public virtual ICollection<Page> Pages { get; set; }

        public virtual ICollection<EpisodeLike> EpisodeLikes { get; set; }

        public virtual ICollection<EpisodeViews> EpisodeViews { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public string WebtoonId { get; set; }

        public virtual Webtoon Webtoon { get; set; }
    }
}
