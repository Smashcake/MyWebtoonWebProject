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

        public ReviewsVotesService(IReviewsVotesRepository reviewsVotesRepository)
        {
            this.reviewsVotesRepository = reviewsVotesRepository;
        }

        public async Task UserVote(string reviewId, bool isUpvote, string userId)
        {
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
