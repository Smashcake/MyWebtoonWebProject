namespace MyWebtoonWebProject.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentDeleteInputModel
    {
        [Required]
        public string CommentNumber { get; set; }
    }
}
