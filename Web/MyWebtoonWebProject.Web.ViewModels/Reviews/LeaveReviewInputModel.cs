namespace MyWebtoonWebProject.Web.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    public class LeaveReviewInputModel
    {
        [Range(1, int.MaxValue)]
        [Required]
        public string WebtoonTitleNumber { get; set; }

        [MaxLength(800)]
        [Required]
        public string UserReview { get; set; }

        public string UserId { get; set; }
    }
}
