namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Collections.Generic;

    using MyWebtoonWebProject.Data.Common.Repositories;
    using MyWebtoonWebProject.Data.Models;

    public interface IReviewsVotesRepository : IRepository<ReviewVote>
    {
        ReviewVote GetReviewVoteByIds(string reviewId, string userId);

        ICollection<ReviewVote> GetReviewVotes(string reviewId);
    }
}
