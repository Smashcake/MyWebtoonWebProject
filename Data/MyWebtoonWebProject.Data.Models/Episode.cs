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
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Page> Pages { get; set; }

        public virtual ICollection<EpisodeLike> EpisodeLikes { get; set; }

        public int Views { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public string WebtoonId { get; set; }

        public Webtoon Webtoon { get; set; }
    }
}
