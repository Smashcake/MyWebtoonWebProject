namespace MyWebtoonWebProject.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Models.Enums;
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
                    ReviewNumber = (this.reviewsRepository.GetReviewsCount() + 1).ToString(),
                };

                await this.reviewsRepository.AddAsync(review);
                await this.reviewsRepository.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("You already gave a review for this webtoon.");
            }
        }

        public ReviewVoteResponseModel ReviewLikesAndDislikes(string reviewNumber)
        {
            var review = this.reviewsRepository.GetReviewByReviewNumber(reviewNumber);
            var reviewLikes = review.ReviewVotes.Sum(rv => rv.Vote.Equals(VoteType.UpVote) ? 1 : 0);
            var reviewDislikes = review.ReviewVotes.Sum(rv => rv.Vote.Equals(VoteType.DownVote) ? 1 : 0);
            return new ReviewVoteResponseModel { Likes = reviewLikes, Dislikes = reviewDislikes };
        }

        public async Task DeleteReview(string reviewNumber, string userId)
        {
            var review = this.reviewsRepository.GetReviewByReviewNumber(reviewNumber);

            if (review.ReviewAuthorId != userId)
            {
                throw new ArgumentException("Invalid action taken!");
            }

            this.reviewsRepository.Delete(review);
            await this.reviewsRepository.SaveChangesAsync();
        }
    }
}
