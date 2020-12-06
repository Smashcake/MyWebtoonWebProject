namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentsVotesService
    {
        Task UserCommentVoteAsync(string commentNumber, bool isUpVote, string userId);
    }
}
