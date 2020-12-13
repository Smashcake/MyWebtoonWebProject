namespace MyWebtoonWebProject.Web.ViewModels.WebtoonsRatings
{
    using System.ComponentModel.DataAnnotations;

    public class WebtoonRatingInputModel
    {
        [Required]
        public string WebtoonTitleNumber { get; set; }

        [Range(1, 5)]
        public int RatingValue { get; set; }
    }
}
