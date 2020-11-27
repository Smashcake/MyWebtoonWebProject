namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    public interface IReviewsVotesService
    {
        Task UserVote(string reviewId, bool isUpvote, string userId);
    }
}
