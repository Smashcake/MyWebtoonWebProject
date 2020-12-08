namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Comments;

    public interface ICommentsVotesService
    {
        Task UserCommentVoteAsync(string commentNumber, bool isUpVote, string userId);

        CommentVoteResponseModel GetCommentVotes(string commentNumber);
    }
}
