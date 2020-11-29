namespace MyWebtoonWebProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyWebtoonWebProject.Data.Common.Models;

    public class Review : BaseDeletableModel<string>
    {
        public Review()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ReviewVotes = new HashSet<ReviewVote>();
        }

        public string ReviewAuthorId { get; set; }

        public ApplicationUser ReviewAuthor { get; set; }

        public string WebtoonId { get; set; }

        public Webtoon Webtoon { get; set; }

        [Required]
        [MaxLength(800)]
        public string ReviewInfo { get; set; }

        public virtual ICollection<ReviewVote> ReviewVotes { get; set; }

        public string ReviewNumber { get; set; }
    }
}
