using Microsoft.AspNetCore.Http;
using Moq;
using MyWebtoonWebProject.Data.Models;
using MyWebtoonWebProject.Data.Repositories;
using MyWebtoonWebProject.Web.ViewModels.Episodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyWebtoonWebProject.Services.Data.Tests
{
    public class EpisodeServiceTests
    {
        [Fact]
        public void GetEpisodeIdWorksCorrectly()
        {
            var episodes = new List<Episode>();
            var episode = new Episode()
            {
                Id = "test",
                Webtoon = new Webtoon()
                {
                    TitleNumber = "123"
                },
                EpisodeNumber = "456"
            };
            episodes.Add(episode);
            var mockEpisodesRepo = new Mock<IEpisodesRepository>();
            mockEpisodesRepo.Setup(x => x.All()).Returns(episodes.AsQueryable());

            var service = new EpisodesService(null, mockEpisodesRepo.Object, null, null, null, null, null, null);

            var result = service.GetEpisodeId("123", "456");

            Assert.Equal("test", result);
        }

        [Fact]
        public void LatestEpisodesWorksCorrectly()
        {
            var firstEpisode = new Episode()
            {
                Webtoon = new Webtoon()
                {
                    Title = "test",
                    TitleNumber = "123",
                    Genre = new Genre()
                    {
                        Name = "action"
                    },
                },
                EpisodeNumber = "456",
                Name = "works",
                CreatedOn = new DateTime(1994, 01, 01),
            };
            var secondEpisode = new Episode()
            {
                Webtoon = new Webtoon()
                {
                    Title = "pesho",
                    TitleNumber = "012",
                    Genre = new Genre()
                    {
                        Name = "gosho"
                    },
                },
                EpisodeNumber = "789",
                Name = "ivan",
                CreatedOn = new DateTime(1998, 01, 01),
            };

            var episodes = new List<Episode>();
            episodes.Add(firstEpisode);
            episodes.Add(secondEpisode);
            var mockEpisodesRepo = new Mock<IEpisodesRepository>();
            mockEpisodesRepo.Setup(x => x.All()).Returns(episodes.AsQueryable());

            var service = new EpisodesService(null, mockEpisodesRepo.Object, null, null, null, null, null, null);

            var result = service.LatestEpisodes();
            var firstResult = result.ToArray()[0];
            var secondResult = result.ToArray()[1];

            Assert.Equal("789", firstResult.EpisodeNumber);
            Assert.Equal("ivan", firstResult.EpisodeTitle);
            Assert.Equal(new DateTime(1998, 01, 01), firstResult.EpisodeCreatedOn);
            Assert.Equal("pesho", firstResult.WebtoonTitle);
            Assert.Equal("012", firstResult.WebtoonTitleNumber);
            Assert.Equal("gosho", firstResult.WebtoonGenreName);

            Assert.Equal("456", secondResult.EpisodeNumber);
            Assert.Equal("works", secondResult.EpisodeTitle);
            Assert.Equal(new DateTime(1994, 01, 01), secondResult.EpisodeCreatedOn);
            Assert.Equal("test", secondResult.WebtoonTitle);
            Assert.Equal("123", secondResult.WebtoonTitleNumber);
            Assert.Equal("action", secondResult.WebtoonGenreName);
        }

        [Fact]
        public async Task DeleteEpisodeAsyncThrowsNullExceptionWhenEpisodeIsNotFound()
        {
            var episodes = new List<Episode>();
            var mockEpisodesRepo = new Mock<IEpisodesRepository>();
            mockEpisodesRepo.Setup(x => x.GetEpisodeByWebtoonTitleNumberAndEpisodeNumber("smt", "smtelse")).Returns(episodes.FirstOrDefault());
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            var webtoon = new Webtoon()
            {
                TitleNumber = "123"
            };
            mockWebtoonsRepo.Setup(x => x.GetWebtoonByTitleNumber("smt")).Returns(webtoon);

            var service = new EpisodesService(mockWebtoonsRepo.Object, mockEpisodesRepo.Object, null, null, null, null, null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.DeleteEpisodeAsync("smt", "smtelse", "user"));
        }

        [Fact]
        public async Task DeleteEpisodeAsyncThrowsNullExceptionWhenAuthorIdIsInvalid()
        {
            var episodes = new List<Episode>();
            var episode = new Episode()
            {
                Webtoon = new Webtoon()
                {
                    AuthorId = "test"
                }
            };
            episodes.Add(episode);
            var mockEpisodesRepo = new Mock<IEpisodesRepository>();
            mockEpisodesRepo.Setup(x => x.GetEpisodeByWebtoonTitleNumberAndEpisodeNumber("smt", "smtelse")).Returns(episodes.FirstOrDefault());
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            var webtoon = new Webtoon()
            {
                TitleNumber = "123",
                AuthorId = "test"
            };
            mockWebtoonsRepo.Setup(x => x.GetWebtoonByTitleNumber("smt")).Returns(webtoon);

            var service = new EpisodesService(mockWebtoonsRepo.Object, mockEpisodesRepo.Object, null, null, null, null, null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.DeleteEpisodeAsync("smt", "smtelse", "user"));
        }

        [Fact]
        public async Task DeleteEpisodeAsyncWorksCorrectly()
        {
            var episodes = new List<Episode>();
            var episode = new Episode()
            {
                Webtoon = new Webtoon()
                {
                    AuthorId = "test"
                }
            };
            episodes.Add(episode);
            var mockEpisodesRepo = new Mock<IEpisodesRepository>();
            mockEpisodesRepo.Setup(x => x.GetEpisodeByWebtoonTitleNumberAndEpisodeNumber("smt", "smtelse")).Returns(episodes.FirstOrDefault());
            mockEpisodesRepo.Setup(x => x.Delete(It.IsAny<Episode>())).Callback((Episode episode) => episode.IsDeleted = true);
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            var webtoon = new Webtoon()
            {
                TitleNumber = "123",
                AuthorId = "test"
            };
            mockWebtoonsRepo.Setup(x => x.GetWebtoonByTitleNumber("smt")).Returns(webtoon);

            var service = new EpisodesService(mockWebtoonsRepo.Object, mockEpisodesRepo.Object, null, null, null, null, null, null);

            await service.DeleteEpisodeAsync("smt", "smtelse", "test");

            Assert.Single(episodes);
            Assert.True(episode.IsDeleted);
        }

        [Fact]
        public async Task AddEpisodeAsyncWorksCorrectly()
        {
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            var webtoon = new Webtoon()
            {
                Title = "test",
                Id = "gosho",
                TitleNumber = "123",
                AuthorId = "test",
                Episodes = new List<Episode>()
            };
            mockWebtoonsRepo.Setup(x => x.GetWebtoonByTitleNumber("123")).Returns(webtoon);
            var episodes = new List<Episode>();
            var mockEpisodesRepo = new Mock<IEpisodesRepository>();
            mockEpisodesRepo.Setup(x => x.GetEpisodesByWebtoonId("gosho")).Returns(episodes);
            mockEpisodesRepo.Setup(x => x.AddAsync(It.IsAny<Episode>())).Callback((Episode episode) => episodes.Add(episode));
            var pages = new List<Page>();
            var mockPageService = new Mock<IPagesService>();
            var episodeInputModel = new AddEpisodeInputModel()
            {
                Pages = new List<IFormFile>(),
                TitleNumber = "123"
            };
            var pathToTestResults = $@"C:\MyWebtoonWebProject\MyWebtoonWebProject\TestResults";

            var service = new EpisodesService(mockWebtoonsRepo.Object, mockEpisodesRepo.Object, null, mockPageService.Object, null, null, null, null);

            await service.AddEpisodeAsync(episodeInputModel, pathToTestResults);

            Assert.Single(episodes);
            Assert.Equal(0,episodes.First().Comments.Count);
            Assert.Equal(0, episodes.First().EpisodeLikes.Count);
            Assert.Equal("1", episodes.First().EpisodeNumber);
            Assert.False(episodes.First().IsDeleted);
            Assert.Equal("Episode1", episodes.First().Name);
            Assert.Equal("gosho", episodes.First().WebtoonId);
        }

        [Fact]
        public void GetEpisodeReturnsCorrectInfo()
        {
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            var webtoon = new Webtoon()
            {
                Title = "test",
                Id = "gosho",
                TitleNumber = "123",
                AuthorId = "test",
                Episodes = new List<Episode>()
            };
            var firstEpisode = new Episode()
            {
                Webtoon = new Webtoon()
                {
                    Title = "test",
                    TitleNumber = "123",
                    Genre = new Genre()
                    {
                        Name = "action"
                    },
                },
                EpisodeNumber = "456",
                Name = "works",
                CreatedOn = new DateTime(1994, 01, 01),
            };
            var secondEpisode = new Episode()
            {
                Webtoon = new Webtoon()
                {
                    Title = "pesho",
                    TitleNumber = "012",
                    Genre = new Genre()
                    {
                        Name = "gosho"
                    },
                },
                EpisodeNumber = "789",
                Name = "ivan",
                CreatedOn = new DateTime(1998, 01, 01),
                Comments = new List<Comment>(),
            };
            secondEpisode.Comments.Add(new Comment()
            {
                CommentAuthorId = "hello",
                CommentNumber = "12345",
                CommentAuthor = new ApplicationUser()
                {
                    UserName = "darkness"
                },
                CommentInfo = "this should work",
                Id = "67890",
                CreatedOn = new DateTime(1999, 01, 01),
                Comments = new List<Comment>(),
                CommentVotes = new List<CommentVote>(),
            });
            var episodes = new List<Episode>();
            episodes.Add(secondEpisode);
            episodes.Add(firstEpisode);
            mockWebtoonsRepo.Setup(x => x.GetWebtoonByTitleNumber("123")).Returns(webtoon);
            var mockEpisodesRepo = new Mock<IEpisodesRepository>();
            mockEpisodesRepo.Setup(x => x.GetEpisodesByWebtoonId("gosho")).Returns(episodes);
            mockEpisodesRepo.Setup(x => x.GetEpisodeByWebtoonTitleNumberAndEpisodeNumber("123", "456")).Returns(episodes.FirstOrDefault());
            var mockApplicationUsersRepo = new Mock<IApplicationUserRepository>();
            var mockPagesService = new Mock<IPagesService>();
            var mockPagesRepo = new Mock<IPagesRepository>();
            var mockEpisodeLikesService = new Mock<IEpisodesLikesService>();
            var mockCommentsRepo = new Mock<ICommentsRepository>();
            mockCommentsRepo.Setup(x => x.GetEpisodeComments(It.IsAny<string>())).Returns(secondEpisode.Comments);
            var mockCommentsVotesRepo = new Mock<ICommentsVotesRepository>();
            mockCommentsVotesRepo.Setup(x => x.GetCommentVotesByCommentId(It.IsAny<string>())).Returns(secondEpisode.Comments.First().CommentVotes);

            var service = new EpisodesService(
                mockWebtoonsRepo.Object,
                mockEpisodesRepo.Object,
                mockPagesRepo.Object, mockPagesService.Object,
                mockEpisodeLikesService.Object, mockCommentsRepo.Object,
                mockApplicationUsersRepo.Object, mockCommentsVotesRepo.Object);

            var result = service.GetEpisode("123", "456");

            Assert.Single(result.Comments);

            Assert.Equal("test", result.EpisodeAuthorId);
            Assert.Equal("hello", result.Comments.First().CommentAuthorId);
            Assert.Null(result.Comments.First().CommentAuthorUsername);
            Assert.Equal("this should work", result.Comments.First().CommentInfo);
            Assert.Equal("12345", result.Comments.First().CommentNumber);
            Assert.Empty(result.Comments.First().CommentReplies);
            Assert.Equal(new DateTime(1999, 01, 01), result.Comments.First().CreatedOn);
            Assert.Equal(0, result.Comments.First().Likes);
            Assert.Equal(0, result.Comments.First().Dislikes);
            Assert.Null(result.Comments.First().ParentId);
            Assert.Equal("67890", result.Comments.First().Id);
            Assert.Equal("789", result.EpisodeNumber);
            Assert.Equal("ivan", result.EpisodeTitle);
            Assert.False(result.HasNextEpisode);
            Assert.False(result.HasPreviousEpisode);
            Assert.Equal("457", result.NextEpisodeNumber);
            Assert.Equal(0, result.Likes);
            Assert.Null(result.PagesPaths);
            Assert.Equal("455", result.PreviousEpisodeNumber);
            Assert.Equal("test", result.WebtoonTitle);
            Assert.Equal("123", result.WebtoonTitleNumber);
        }
    }
}
