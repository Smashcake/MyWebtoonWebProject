namespace MyWebtoonWebProject.Web.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    public class ReviewVoteInputModel
    {
        [Required]
        public string ReviewNumber { get; set; }

        public bool IsUpVote { get; set; }
    }
}
