namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Reviews;

    public interface IReviewsService
    {
        Task AddReviewAsync(LeaveReviewInputModel input);

        Task DeleteReviewAsync(string reviewNumber, string userId);

        Task EditReviewAsync(string reviewNumber, string userId, string reviewInfo);

        ReviewVoteResponseModel ReviewLikesAndDislikes(string reviewId);
    }
}
