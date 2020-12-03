namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentsVotesService
    {
        Task UserCommentVote(string commentNumber, bool isUpVote, string userId);
    }
}
