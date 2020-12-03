namespace MyWebtoonWebProject.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Models.Enums;
    using MyWebtoonWebProject.Data.Repositories;

    public class ReviewsVotesService : IReviewsVotesService
    {
        private readonly IReviewsVotesRepository reviewsVotesRepository;
        private readonly IReviewsRepository reviewsRepository;

        public ReviewsVotesService(IReviewsVotesRepository reviewsVotesRepository, IReviewsRepository reviewsRepository)
        {
            this.reviewsVotesRepository = reviewsVotesRepository;
            this.reviewsRepository = reviewsRepository;
        }

        public async Task UserReviewVote(string reviewNumber, bool isUpVote, string userId)
        {
            var reviewId = this.reviewsRepository.GetReviewByReviewNumber(reviewNumber).Id;

            var userReviewVote = this.reviewsVotesRepository.GetReviewVoteByIds(reviewId, userId);
            if (userReviewVote != null)
            {
                userReviewVote.Vote = isUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                userReviewVote = new ReviewVote
                {
                    ReviewId = reviewId,
                    ApplicationUserId = userId,
                    Vote = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.reviewsVotesRepository.AddAsync(userReviewVote);
            }

            await this.reviewsVotesRepository.SaveChangesAsync();
        }
    }
}
