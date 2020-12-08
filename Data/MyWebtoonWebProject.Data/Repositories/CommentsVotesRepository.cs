namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Collections.Generic;
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

        public ICollection<CommentVote> GetCommentVotesByCommentId(string commentId)
        {
            return this.DbSet.Where(cv => cv.CommentId == commentId).ToList();
        }
    }
}
