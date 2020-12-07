namespace MyWebtoonWebProject.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentDeleteInputModel
    {
        [Required]
        public string CommentNumber { get; set; }

        [Required]
        public string WebtoonTitleNumber { get; set; }

        [Required]
        public string EpisodeNumber { get; set; }
    }
}
