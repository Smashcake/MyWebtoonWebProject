namespace MyWebtoonWebProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyWebtoonWebProject.Data.Repositories;
    using MyWebtoonWebProject.Web.ViewModels.Reviews;
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public class ApplicationUsersService : IAppicationUsersService
    {
        private readonly IWebtoonsRepository webtoonsRepository;
        private readonly IReviewsRepository reviewsRepository;

        public ApplicationUsersService(IWebtoonsRepository webtoonsRepository, IReviewsRepository reviewsRepository)
        {
            this.webtoonsRepository = webtoonsRepository;
            this.reviewsRepository = reviewsRepository;
        }

        public ICollection<GetWebtoonInfoViewModel> GetUserSubscribtions(string userId)
        {
            var webtoonsInfo = this.webtoonsRepository
                .AllAsNoTracking()
                .Where(w => w.Subscribers.Any(s => s.SubscriberId == userId))
                .Select(w => new GetWebtoonInfoViewModel
                {
                    Author = w.Author.UserName,
                    CoverPhoto = w.CoverPhoto,
                    Genre = w.Genre.Name,
                    Likes = w.Episodes.Sum(e => e.Likes),
                    Title = w.Title,
                    TitleNumber = w.TitleNumber,
                }).ToList();

            return webtoonsInfo;
        }

        public ICollection<ApplicationUserReviewViewModel> GetUserReviews(string userId)
        {
            var reviewsInfo = this.reviewsRepository
                .AllAsNoTracking()
                .Where(r => r.ReviewAuthorId == userId)
                .Select(r => new ApplicationUserReviewViewModel
                {
                    CreatedOn = r.CreatedOn,
                    WebtoonTitle = r.Webtoon.Title,
                    ReviewInfo = r.ReviewInfo,
                    Likes = r.Likes,
                    Dislikes = r.Dislikes,
                }).ToList();

            return reviewsInfo;
        }
    }
}
