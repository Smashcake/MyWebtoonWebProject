namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Reviews;

    public interface IReviewsService
    {
        Task AddReview(LeaveReviewInputModel input);

        Task DeleteReview(string reviewNumber, string userId);

        ReviewVoteResponseModel ReviewLikesAndDislikes(string reviewId);
    }
}
