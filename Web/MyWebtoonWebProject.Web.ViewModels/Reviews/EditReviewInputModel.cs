namespace MyWebtoonWebProject.Web.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    public class EditReviewInputModel
    {
        [Required]
        [MaxLength(800)]
        public string ReviewInfo { get; set; }

        [Required]
        public string ReviewNumber { get; set; }
    }
}
