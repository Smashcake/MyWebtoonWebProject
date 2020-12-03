namespace MyWebtoonWebProject.Data.Repositories
{
    using MyWebtoonWebProject.Data.Common.Repositories;
    using MyWebtoonWebProject.Data.Models;

    public interface ICommentsVotesRepository : IRepository<CommentVote>
    {
        CommentVote GetCommentVoteByIds(string commentId, string userId);
    }
}
