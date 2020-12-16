namespace MyWebtoonWebProject.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using Xunit;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Repositories;
    using MyWebtoonWebProject.Web.ViewModels.Comments;
    using System;

    public class CommentsServiceTests
    {
        [Fact]
        public async Task CreateCommentAsyncShouldReturnCorrectValuesWhenGivenValidInputWithoutParentId()
        {
            var commentsList = new List<Comment>();
            var episodesList = new List<Episode>();
            episodesList.Add(new Episode()
            {
                Webtoon = new Webtoon { TitleNumber = "hello" },
                EpisodeNumber = "darkness",
                Id = "test123"
            });
            var webtoonTitleNumber = "hello";
            var episodeNumber = "darkness";
            var mockCommentsRepo = new Mock<ICommentsRepository>();
            mockCommentsRepo.Setup(x => x.All()).Returns(commentsList.AsQueryable());
            mockCommentsRepo.Setup(x => x.AddAsync(It.IsAny<Comment>())).Callback((Comment comment) => commentsList.Add(comment));
            var mockEpisodesService = new Mock<IEpisodesService>();
            mockEpisodesService.Setup(x => x.GetEpisodeId(webtoonTitleNumber, episodeNumber))
                .Returns(episodesList.FirstOrDefault(e => e.Webtoon.TitleNumber == webtoonTitleNumber && e.EpisodeNumber == episodeNumber).Id);

            var service = new CommentsService(mockCommentsRepo.Object, mockEpisodesService.Object);

            var test = new CommentInputModel()
            {
                EpisodeNumber = "darkness",
                UserComment = "my old friend",
                WebtoonTitleNumber = "hello",
                ParentId = null,
            };
            await service.CreateCommentAsync(test, "test");

            Assert.Single(commentsList);
            Assert.Equal("test123", commentsList.First().EpisodeId);
            Assert.Equal("test", commentsList.First().CommentAuthorId);
            Assert.Equal("my old friend", commentsList.First().CommentInfo);
        }

        [Fact]
        public async Task CreateCommentAsyncShouldReturnCorrectValuesWhenGivenValidInputWithParentId()
        {
            var commentsList = new List<Comment>();
            var episodesList = new List<Episode>();
            episodesList.Add(new Episode()
            {
                Webtoon = new Webtoon { TitleNumber = "hello" },
                EpisodeNumber = "darkness",
                Id = "test123"
            });
            var webtoonTitleNumber = "hello";
            var episodeNumber = "darkness";
            var mockCommentsRepo = new Mock<ICommentsRepository>();
            mockCommentsRepo.Setup(x => x.All()).Returns(commentsList.AsQueryable());
            mockCommentsRepo.Setup(x => x.AddAsync(It.IsAny<Comment>())).Callback((Comment comment) => commentsList.Add(comment));
            var mockEpisodesService = new Mock<IEpisodesService>();
            mockEpisodesService.Setup(x => x.GetEpisodeId(webtoonTitleNumber, episodeNumber))
                .Returns(episodesList.FirstOrDefault(e => e.Webtoon.TitleNumber == webtoonTitleNumber && e.EpisodeNumber == episodeNumber).Id);

            var service = new CommentsService(mockCommentsRepo.Object, mockEpisodesService.Object);

            commentsList.Add(new Comment()
            {
                Id = "test123"
            });

            var test = new CommentInputModel()
            {
                EpisodeNumber = "darkness",
                UserComment = "my old friend",
                WebtoonTitleNumber = "hello",
                ParentId = "test123",
            };
            await service.CreateCommentAsync(test, "test");

            var comment = commentsList.FirstOrDefault(x => x.ParentId == "test123");
            Assert.Equal(2, commentsList.Count());
            Assert.Equal("test123", comment.EpisodeId);
            Assert.Equal("test", comment.CommentAuthorId);
            Assert.Equal("my old friend", comment.CommentInfo);
            Assert.Equal("test123", comment.ParentId);
        }

        [Fact]
        public async Task DeleteCommentAsyncWorksWithCorrectInfo()
        {
            var commentsList = new List<Comment>();
            var commentNumber = "test123";
            var comment = new Comment()
            {
                CommentNumber = "test123",
                CommentAuthorId = "pesho",
            };

            commentsList.Add(comment);

            var mockCommentsRepo = new Mock<ICommentsRepository>();
            mockCommentsRepo.Setup(x => x.GetCommentByCommentNumber(commentNumber)).Returns(commentsList.FirstOrDefault(x => x.CommentNumber == commentNumber));
            mockCommentsRepo.Setup(x => x.Delete(It.IsAny<Comment>())).Callback((Comment comment) => comment.IsDeleted = true);

            var service = new CommentsService(mockCommentsRepo.Object, null);

            await service.DeleteCommentAsync("test123", "pesho");

            Assert.Single(commentsList);
            Assert.True(commentsList.First().IsDeleted);
        }

        [Fact]
        public async Task DeleteCommentAsyncThrowsArgumentErrorWhenGivenInvalidAuthorId()
        {
            var commentsList = new List<Comment>();
            var commentNumber = "test123";
            var comment = new Comment()
            {
                CommentNumber = "test123",
                CommentAuthorId = "pesho",
            };

            commentsList.Add(comment);

            var mockCommentsRepo = new Mock<ICommentsRepository>();
            mockCommentsRepo.Setup(x => x.GetCommentByCommentNumber(commentNumber)).Returns(commentsList.FirstOrDefault(x => x.CommentNumber == commentNumber));
            mockCommentsRepo.Setup(x => x.Delete(It.IsAny<Comment>())).Callback((Comment comment) => comment.IsDeleted = true);

            var service = new CommentsService(mockCommentsRepo.Object, null);

            await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteCommentAsync("test123", "gosho"));
        }

        [Fact]
        public async Task EditCommentAsyncShouldEditCommentInfoWhenGivenValidInput()
        {
            var commentsList = new List<Comment>();
            var commentNumber = "test123";
            var comment = new Comment()
            {
                CommentNumber = "test123",
                CommentAuthorId = "pesho",
            };

            commentsList.Add(comment);

            var mockCommentsRepo = new Mock<ICommentsRepository>();
            mockCommentsRepo.Setup(x => x.GetCommentByCommentNumber(commentNumber)).Returns(commentsList.FirstOrDefault(x => x.CommentNumber == commentNumber));

            var service = new CommentsService(mockCommentsRepo.Object, null);

            await service.EditCommentAsync("test123", "pesho", "this sucks");

            Assert.Equal("test123", commentsList.First().CommentNumber);
            Assert.Equal("pesho", commentsList.First().CommentAuthorId);
            Assert.Equal("this sucks", commentsList.First().CommentInfo);
        }

        [Fact]
        public async Task EditCommentAsyncShouldThrowsArgumentErrorWhenGivenInvalidAuthorId()
        {
            var commentsList = new List<Comment>();
            var commentNumber = "test123";
            var comment = new Comment()
            {
                CommentNumber = "test123",
                CommentAuthorId = "pesho",
            };

            commentsList.Add(comment);

            var mockCommentsRepo = new Mock<ICommentsRepository>();
            mockCommentsRepo.Setup(x => x.GetCommentByCommentNumber(commentNumber)).Returns(commentsList.FirstOrDefault(x => x.CommentNumber == commentNumber));

            var service = new CommentsService(mockCommentsRepo.Object, null);

            await Assert.ThrowsAsync<ArgumentException>(() => service.EditCommentAsync("test123", "gosho", "this sucks"));
        }
    }
}
