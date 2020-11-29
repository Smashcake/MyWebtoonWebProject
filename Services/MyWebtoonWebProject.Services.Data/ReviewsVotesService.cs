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

        public async Task UserVote(string reviewNumber, bool isUpvote, string userId)
        {
            var reviewId = this.reviewsRepository.GetReviewByReviewNumber(reviewNumber).Id;

            var userVote = this.reviewsVotesRepository.GetReviewVoteByIds(reviewId, userId);
            if (userVote != null)
            {
                userVote.Vote = isUpvote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                userVote = new ReviewVote
                {
                    ReviewId = reviewId,
                    ApplicationUserId = userId,
                    Vote = isUpvote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.reviewsVotesRepository.AddAsync(userVote);
            }

            await this.reviewsVotesRepository.SaveChangesAsync();
        }
    }
}
