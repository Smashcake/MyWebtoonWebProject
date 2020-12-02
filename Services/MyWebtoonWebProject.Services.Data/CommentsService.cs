﻿namespace MyWebtoonWebProject.Services.Data
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

        public async Task CreateComment(CommentInputModel input, string userId)
        {
            var episodeId = this.episodesService.GetEpisodeId(input.WebtoonTitleNumber, input.EpisodeNumber);
            var comment = new Comment
            {
                CommentAuthorId = userId,
                EpisodeId = episodeId,
                PostedOn = DateTime.UtcNow,
                CommentInfo = input.UserComment,
                CommentNumber = (this.commentsRepository.Count() + 1).ToString(),
                ParentId = input.ParentId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }
    }
}
