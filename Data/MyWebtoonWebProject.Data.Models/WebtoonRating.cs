namespace MyWebtoonWebProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyWebtoonWebProject.Data.Common.Models;

    public class WebtoonRating : BaseModel<string>
    {
        public WebtoonRating()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string WebtoonId { get; set; }

        public virtual Webtoon Webtoon { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public byte RatingValue { get; set; }
    }
}
