namespace MyWebtoonWebProject.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentVoteInputModel
    {
        [Required]
        public string CommentNumber { get; set; }

        public bool IsUpVote { get; set; }
    }
}
