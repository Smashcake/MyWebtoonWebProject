namespace MyWebtoonWebProject.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentInputModel
    {
        [Required]
        public string WebtoonTitleNumber { get; set; }

        [Required]
        public string EpisodeNumber { get; set; }

        [Required]
        [MaxLength(300)]
        public string UserComment { get; set; }

        public string ParentId { get; set; }
    }
}
