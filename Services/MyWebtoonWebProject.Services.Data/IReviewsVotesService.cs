namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    public interface IReviewsVotesService
    {
        Task UserReviewVote(string reviewId, bool isUpvote, string userId);
    }
}
