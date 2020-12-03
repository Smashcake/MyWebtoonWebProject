namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Linq;

    using MyWebtoonWebProject.Data.Models;

    public class CommentsVotesRepository : EfRepository<CommentVote>, ICommentsVotesRepository
    {
        public CommentsVotesRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public CommentVote GetCommentVoteByIds(string commentId, string userId)
        {
            return this.DbSet.FirstOrDefault(cv => cv.CommentId == commentId && cv.ApplicationUserId == userId);
        }
    }
}
