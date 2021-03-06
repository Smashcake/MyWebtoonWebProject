﻿namespace MyWebtoonWebProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Models.Enums;
    using MyWebtoonWebProject.Data.Repositories;
    using MyWebtoonWebProject.Web.ViewModels.Comments;
    using MyWebtoonWebProject.Web.ViewModels.Episodes;

    public class EpisodesService : IEpisodesService
    {
        private readonly IWebtoonsRepository webtoonsRepository;
        private readonly IEpisodesRepository episodesRepository;
        private readonly IPagesRepository pagesRepository;
        private readonly IPagesService pagesService;
        private readonly IEpisodesLikesService episodesLikesService;
        private readonly ICommentsRepository commentsRepository;
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly ICommentsVotesRepository commentsVotesRepository;

        public EpisodesService(
            IWebtoonsRepository webtoonsRepository,
            IEpisodesRepository episodesRepository,
            IPagesRepository pagesRepository,
            IPagesService pagesService,
            IEpisodesLikesService episodesLikesService,
            ICommentsRepository commentsRepository,
            IApplicationUserRepository applicationUserRepository,
            ICommentsVotesRepository commentsVotesRepository)
        {
            this.webtoonsRepository = webtoonsRepository;
            this.episodesRepository = episodesRepository;
            this.pagesRepository = pagesRepository;
            this.pagesService = pagesService;
            this.episodesLikesService = episodesLikesService;
            this.commentsRepository = commentsRepository;
            this.applicationUserRepository = applicationUserRepository;
            this.commentsVotesRepository = commentsVotesRepository;
        }

        public async Task AddEpisodeAsync(AddEpisodeInputModel input, string webRootPath)
        {
            var webtoon = this.webtoonsRepository.GetWebtoonByTitleNumber(input.TitleNumber);
            webtoon.Episodes = this.episodesRepository.GetEpisodesByWebtoonId(webtoon.Id);
            var episodesCount = webtoon.Episodes.Count + 1;
            string episodeName = "Episode" + episodesCount;
            string topFolder = $@"{webRootPath}\Webtoons\{webtoon.Title}";
            string episodeFolder = Path.Combine(topFolder, episodeName);
            Directory.CreateDirectory(episodeFolder);

            var episode = new Episode
            {
                Name = episodeName,
                WebtoonId = webtoon.Id,
                IsDeleted = false,
                CreatedOn = DateTime.UtcNow,
                EpisodeNumber = episodesCount.ToString(),
            };

            episode.Pages = this.pagesService.AddPagesAsync(input.Pages, episodeFolder, episode.Id).Result;

            await this.episodesRepository.AddAsync(episode);
            await this.episodesRepository.SaveChangesAsync();
        }

        public GetEpisodeViewModel GetEpisode(string webtoonTitleNumber, string episodeNumber)
        {
            var nextEpisodeNumber = int.Parse(episodeNumber) + 1;
            var previousEpisodeNumber = int.Parse(episodeNumber) - 1;
            var viewModel = new GetEpisodeViewModel();
            var webtoon = this.webtoonsRepository.GetWebtoonByTitleNumber(webtoonTitleNumber);
            webtoon.Episodes = this.episodesRepository.GetEpisodesByWebtoonId(webtoon.Id);
            var episode = this.episodesRepository.GetEpisodeByWebtoonTitleNumberAndEpisodeNumber(webtoonTitleNumber, episodeNumber);
            viewModel.EpisodeAuthorId = webtoon.AuthorId;
            viewModel.EpisodeTitle = episode.Name;
            viewModel.EpisodeNumber = episode.EpisodeNumber;
            viewModel.WebtoonTitleNumber = webtoon.TitleNumber;
            viewModel.WebtoonTitle = webtoon.Title;
            viewModel.PagesPaths = this.pagesRepository.GetPagePathsForEpisodeByEpisodeId(episode.Id);
            viewModel.Likes = this.episodesLikesService.GetEpisodeLikes(episode.Id);
            viewModel.HasNextEpisode = webtoon.Episodes.Any(e => e.EpisodeNumber == nextEpisodeNumber.ToString());
            viewModel.HasPreviousEpisode = webtoon.Episodes.Any(e => e.EpisodeNumber == previousEpisodeNumber.ToString());
            viewModel.NextEpisodeNumber = nextEpisodeNumber.ToString();
            viewModel.PreviousEpisodeNumber = previousEpisodeNumber.ToString();
            viewModel.Comments = this.commentsRepository.GetEpisodeComments(episode.Id).Select(c => new EpisodeCommentViewModel
            {
                ParentId = c.ParentId,
                CommentAuthorUsername = this.applicationUserRepository.GetUsersUsernameById(c.CommentAuthorId),
                CommentAuthorId = c.CommentAuthorId,
                CommentNumber = c.CommentNumber,
                CommentInfo = c.CommentInfo,
                CreatedOn = c.CreatedOn,
                Likes = this.commentsVotesRepository.GetCommentVotesByCommentId(c.Id).Sum(cv => cv.Vote.Equals(VoteType.UpVote) ? 1 : 0),
                Dislikes = this.commentsVotesRepository.GetCommentVotesByCommentId(c.Id).Sum(cv => cv.Vote.Equals(VoteType.DownVote) ? 1 : 0),
                Id = c.Id,
                CommentReplies = c.Comments.Select(r => new CommentReplyViewModel
                {
                    CommentAuthorUsername = this.applicationUserRepository.GetUsersUsernameById(r.CommentAuthorId),
                    CommentAuthorId = r.CommentAuthorId,
                    CommentInfo = r.CommentInfo,
                    CommentNumber = r.CommentNumber,
                    CreatedOn = r.CreatedOn,
                    ParentId = r.ParentId,
                    Likes = this.commentsVotesRepository.GetCommentVotesByCommentId(r.Id).Sum(rv => rv.Vote.Equals(VoteType.UpVote) ? 1 : 0),
                    Dislikes = this.commentsVotesRepository.GetCommentVotesByCommentId(r.Id).Sum(rv => rv.Vote.Equals(VoteType.DownVote) ? 1 : 0),
                })
                .ToList(),
            })
                .ToList();
            return viewModel;
        }

        public string GetEpisodeId(string webtoonTitleNumber, string episodeNumber)
        {
            return this.episodesRepository.All().FirstOrDefault(e => e.Webtoon.TitleNumber == webtoonTitleNumber && e.EpisodeNumber == episodeNumber).Id;
        }

        public async Task DeleteEpisodeAsync(string webtoonTitleNumber, string episodeNumber, string userId)
        {
            var webtoon = this.webtoonsRepository.GetWebtoonByTitleNumber(webtoonTitleNumber);
            var episode = this.episodesRepository.GetEpisodeByWebtoonTitleNumberAndEpisodeNumber(webtoonTitleNumber, episodeNumber);

            if (episode == null || webtoon.AuthorId != userId)
            {
                throw new ArgumentNullException("Invalid data!");
            }

            this.episodesRepository.Delete(episode);
            await this.episodesRepository.SaveChangesAsync();
        }

        public ICollection<LatestEpisodeViewModel> LatestEpisodes()
        {
            return this.episodesRepository.All()
                .OrderByDescending(e => e.CreatedOn)
                .Take(10)
                .Select(e => new LatestEpisodeViewModel
                {
                    WebtoonCoverPhoto = e.Webtoon.CoverPhoto,
                    WebtoonTitle = e.Webtoon.Title,
                    WebtoonTitleNumber = e.Webtoon.TitleNumber,
                    WebtoonGenreName = e.Webtoon.Genre.Name,
                    EpisodeTitle = e.Name,
                    EpisodeNumber = e.EpisodeNumber,
                    EpisodeCreatedOn = e.CreatedOn,
                }).ToList();
        }
    }
}
