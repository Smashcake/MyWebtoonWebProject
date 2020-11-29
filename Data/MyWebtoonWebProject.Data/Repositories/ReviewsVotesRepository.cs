namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using MyWebtoonWebProject.Data.Models;

    public class ReviewsVotesRepository : EfRepository<ReviewVote>, IReviewsVotesRepository
    {
        public ReviewsVotesRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public ReviewVote GetReviewVoteByIds(string reviewId, string userId)
        {
            return this.DbSet.FirstOrDefault(rv => rv.ReviewId == reviewId && rv.ApplicationUserId == userId);
        }

        public ICollection<ReviewVote> GetReviewVotes(string reviewId)
        {
            return this.DbSet.Where(rv => rv.ReviewId == reviewId).ToList();
        }
    }
}
