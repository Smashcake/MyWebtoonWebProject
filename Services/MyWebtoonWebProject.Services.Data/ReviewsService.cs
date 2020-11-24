namespace MyWebtoonWebProject.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Repositories;
    using MyWebtoonWebProject.Web.ViewModels.Reviews;

    public class ReviewsService : IReviewsService
    {
        private readonly IReviewsRepository reviewsRepository;
        private readonly IWebtoonsRepository webtoonsRepository;

        public ReviewsService(IReviewsRepository reviewsRepository, IWebtoonsRepository webtoonsRepository)
        {
            this.reviewsRepository = reviewsRepository;
            this.webtoonsRepository = webtoonsRepository;
        }

        public async Task AddReview(LeaveReviewInputModel input)
        {
            var webtoonId = this.webtoonsRepository.GetWebtoonByTitleNumber(input.WebtoonTitleNumber).Id;
            var review = this.reviewsRepository.All().FirstOrDefault(r => r.ReviewAuthorId == input.UserId && r.WebtoonId == webtoonId);

            if (review == null)
            {
                review = new Review
                {
                    CreatedOn = DateTime.UtcNow,
                    ReviewAuthorId = input.UserId,
                    WebtoonId = webtoonId,
                    ReviewInfo = input.UserReview,
                };

                await this.reviewsRepository.AddAsync(review);
                await this.reviewsRepository.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("You already gave a review for this webtoon.");
            }
        }
    }
}
