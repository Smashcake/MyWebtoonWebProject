namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    public interface IReviewsVotesService
    {
        Task UserReviewVoteAsync(string reviewId, bool isUpvote, string userId);
    }
}
