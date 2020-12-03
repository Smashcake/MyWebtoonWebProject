namespace MyWebtoonWebProject.Services.Data
{
    using System.Collections.Generic;

    using MyWebtoonWebProject.Web.ViewModels.Comments;
    using MyWebtoonWebProject.Web.ViewModels.Reviews;
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public interface IAppicationUsersService
    {
        ICollection<GetWebtoonInfoViewModel> GetUserSubscribtions(string userId);

        ICollection<ApplicationUserReviewViewModel> GetUserReviews(string userId);

        ICollection<ApplicationUserCommentViewModel> GetUserComments(string userId);
    }
}
