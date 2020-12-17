using Moq;
using MyWebtoonWebProject.Data.Models;
using MyWebtoonWebProject.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MyWebtoonWebProject.Services.Data.Tests
{
    public class ApplicationUsersServiceTests
    {
        [Fact]
        public void GetUserSubscribtionsWorksCorrectly()
        {
            var webtoons = new List<Webtoon>();
            var webtoon = new Webtoon()
            {
                Id = "789",
                AuthorId = "test",
                Author = new ApplicationUser()
                {
                    UserName = "pesho"
                },
                Genre = new Genre()
                {
                    Name = "action"
                },
                Title = "work",
                TitleNumber = "123"
            };
            webtoon.Subscribers.Add(new WebtoonsSubscribers()
            {
                SubscriberId = "456",
                WebtoonId = "789"
            });
            var episode = new Episode()
            {
                Id = "012",
                WebtoonId = "789",
                EpisodeLikes = new List<EpisodeLike>(),
            };
            episode.EpisodeLikes.Add(new EpisodeLike()
            {
                EpisodeId = "012",
                ApplicationUserId = "ivan",
                HasLiked = true
            });
            webtoon.Episodes.Add(episode);
            webtoons.Add(webtoon);
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            mockWebtoonsRepo.Setup(x => x.All()).Returns(webtoons.AsQueryable());
            var mockEpisodesRepo = new Mock<IEpisodesRepository>();
            mockEpisodesRepo.Setup(x => x.GetEpisodesByWebtoonId("789")).Returns(webtoon.Episodes);
            var mockEpisodeLikesService = new Mock<IEpisodesLikesService>();
            mockEpisodeLikesService.Setup(x => x.GetEpisodeLikes("012")).Returns(episode.EpisodeLikes.Sum(x => x.HasLiked ? 1 : 0));

            var service = new ApplicationUsersService(mockWebtoonsRepo.Object, null, null, null, mockEpisodeLikesService.Object, mockEpisodesRepo.Object);

            var result = service.GetUserSubscribtions("456");

            Assert.Equal("pesho", result.First().Author);
            Assert.Equal(1,result.First().Episodes.Count);
            Assert.Equal("action", result.First().Genre);
            Assert.Equal("work", result.First().Title);
            Assert.Equal("123", result.First().TitleNumber);
            Assert.Equal(1, result.First().Likes);
        }

        [Fact]
        public void GetUserReviewsWorksCorrectly()
        {
            var reviews = new List<Review>();
            var dateTime = new DateTime(1994, 01, 01);
            var review = new Review()
            {
                Id = "012",
                ReviewAuthorId = "gosho",
                ReviewNumber = "123",
                ReviewInfo = "work",
                CreatedOn = dateTime,
                WebtoonId = "456",
                Webtoon = new Webtoon()
                {
                    Title = "test",
                    TitleNumber = "789"
                },
                ReviewVotes = new List<ReviewVote>()
            };
            var reviewVote = new ReviewVote()
            {
                ApplicationUserId = "ivan",
                ReviewId = "012",
                Vote = MyWebtoonWebProject.Data.Models.Enums.VoteType.UpVote
            };
            review.ReviewVotes.Add(reviewVote);
            reviews.Add(review);
            var mockReviewRepo = new Mock<IReviewsRepository>();
            mockReviewRepo.Setup(x => x.All()).Returns(reviews.AsQueryable());

            var service = new ApplicationUsersService(null, mockReviewRepo.Object, null, null, null, null);

            var result = service.GetUserReviews("gosho");

            Assert.Equal(new DateTime(1994, 01, 01), result.First().CreatedOn);
            Assert.Equal(0, result.First().Dislikes);
            Assert.Equal(1, result.First().Likes);
            Assert.Equal("gosho", result.First().ReviewAuthorId);
            Assert.Equal("123", result.First().ReviewNumber);
            Assert.Equal("789", result.First().WebtoonTitleNumber);
            Assert.Equal("test", result.First().WebtoonTitle);
            Assert.Equal("work", result.First().ReviewInfo);
        }

        [Fact]
        public void GetUserCommentsWorksCorrectly()
        {
            var comments = new List<Comment>();
            var comment = new Comment()
            {
                CommentAuthorId = "gosho",
                CommentInfo = "test",
                CommentNumber = "123",
                Id = "456",
                CreatedOn = new DateTime(1994, 01, 01),
                Episode = new Episode()
                {
                    EpisodeNumber = "789",
                    Webtoon = new Webtoon()
                    {
                        Title = "works",
                        TitleNumber = "012",
                    }
                },
                CommentVotes = new List<CommentVote>(),
            };
            var commentVote = new CommentVote()
            {
                CommentId = "456",
                ApplicationUserId = "ivan",
                Vote = MyWebtoonWebProject.Data.Models.Enums.VoteType.UpVote
            };
            var secondCommentVote = new CommentVote()
            {
                CommentId = "456",
                ApplicationUserId = "pesho",
                Vote = MyWebtoonWebProject.Data.Models.Enums.VoteType.DownVote
            };
            comment.CommentVotes.Add(commentVote);
            comment.CommentVotes.Add(secondCommentVote);
            comments.Add(comment);
            var mockCommentsRepo = new Mock<ICommentsRepository>();
            mockCommentsRepo.Setup(x => x.All()).Returns(comments.AsQueryable());
            var mockCommentsVote = new Mock<ICommentsVotesRepository>();
            var commentVotes = new List<CommentVote>();
            commentVotes.Add(commentVote);
            commentVotes.Add(secondCommentVote);

            var service = new ApplicationUsersService(null, null, mockCommentsRepo.Object, mockCommentsVote.Object, null, null);

            var result = service.GetUserComments("gosho");

            Assert.Equal("gosho", result.First().CommentAuthorId);
            Assert.Equal("456", result.First().CommentId);
            Assert.Equal("test", result.First().CommentInfo);
            Assert.Equal("123", result.First().CommentNumber);
            Assert.Equal(2, result.First().CommentVotes.Count);
            Assert.Equal(new DateTime(1994, 01, 01), result.First().CreatedOn);
            Assert.Equal(1, result.First().Likes);
            Assert.Equal(1, result.First().Dislikes);
            Assert.Equal("789", result.First().EpisodeNumber);
            Assert.Equal("works", result.First().WebtoonTitle);
            Assert.Equal("012", result.First().WebtoonTitleNumber);
        }
    }
}
