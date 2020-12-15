namespace MyWebtoonWebProject.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentEditInputModel
    {
        [Required]
        [MaxLength(300)]
        public string CommentInfo { get; set; }

        [Required]
        public string CommentNumber { get; set; }
    }
}
