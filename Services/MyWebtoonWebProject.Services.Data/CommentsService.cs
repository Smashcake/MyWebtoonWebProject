namespace MyWebtoonWebProject.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Repositories;
    using MyWebtoonWebProject.Web.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository commentsRepository;
        private readonly IEpisodesService episodesService;

        public CommentsService(ICommentsRepository commentsRepository, IEpisodesService episodesService)
        {
            this.commentsRepository = commentsRepository;
            this.episodesService = episodesService;
        }

        public async Task CreateCommentAsync(CommentInputModel input, string userId)
        {
            var episodeId = this.episodesService.GetEpisodeId(input.WebtoonTitleNumber, input.EpisodeNumber);
            var comment = new Comment
            {
                CommentAuthorId = userId,
                EpisodeId = episodeId,
                CreatedOn = DateTime.UtcNow,
                CommentInfo = input.UserComment,
                CommentNumber = (this.commentsRepository.Count() + 1).ToString(),
                ParentId = input.ParentId,
            };

            if (comment.ParentId != null)
            {
                var parentComment = this.commentsRepository.All().FirstOrDefault(c => c.Id == comment.ParentId);
                parentComment.Comments.Add(comment);
            }

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(string commentNumber, string userId)
        {
            var comment = this.commentsRepository.GetCommentByCommentNumber(commentNumber);

            if (comment.CommentAuthorId != userId)
            {
                throw new ArgumentException("Invalid action taken!");
            }
            else
            {
                this.commentsRepository.Delete(comment);
            }

            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task EditCommentAsync(string commentNumber, string userId, string commentInfo)
        {
            var comment = this.commentsRepository.GetCommentByCommentNumber(commentNumber);

            if (comment.CommentAuthorId != userId)
            {
                throw new ArgumentException("Invalid action taken!");
            }
            else
            {
                comment.CommentInfo = commentInfo;
            }

            await this.commentsRepository.SaveChangesAsync();
        }
    }
}
