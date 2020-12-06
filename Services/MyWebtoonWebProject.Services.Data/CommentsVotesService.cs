namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Models.Enums;
    using MyWebtoonWebProject.Data.Repositories;

    public class CommentsVotesService : ICommentsVotesService
    {
        private readonly ICommentsRepository commentsRepository;
        private readonly ICommentsVotesRepository commentsVotesRepository;

        public CommentsVotesService(ICommentsRepository commentsRepository, ICommentsVotesRepository commentsVotesRepository)
        {
            this.commentsRepository = commentsRepository;
            this.commentsVotesRepository = commentsVotesRepository;
        }

        public async Task UserCommentVoteAsync(string commentNumber, bool isUpVote, string userId)
        {
            var commentId = this.commentsRepository.GetCommentByCommentNumber(commentNumber).Id;

            var userCommentVote = this.commentsVotesRepository.GetCommentVoteByIds(commentId, userId);
            if (userCommentVote != null)
            {
                userCommentVote.Vote = isUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                userCommentVote = new CommentVote
                {
                    CommentId = commentId,
                    ApplicationUserId = userId,
                    Vote = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.commentsVotesRepository.AddAsync(userCommentVote);
            }

            await this.commentsVotesRepository.SaveChangesAsync();
        }
    }
}
