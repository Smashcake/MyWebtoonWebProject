namespace MyWebtoonWebProject.Web.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    public class DeleteReviewInputModel
    {
        [Required]
        public string ReviewNumber { get; set; }

    }
}
