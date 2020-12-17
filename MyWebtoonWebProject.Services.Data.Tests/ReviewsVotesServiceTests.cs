using Moq;
using MyWebtoonWebProject.Data.Models;
using MyWebtoonWebProject.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyWebtoonWebProject.Services.Data.Tests
{
    public class ReviewsVotesServiceTests
    {
        [Fact]
        public async Task UserReviewVoteAsyncChangesVoteCorrectly()
        {
            var reviews = new List<Review>();
            var review = new Review()
            {
                Id = "test",
                ReviewAuthorId = "gosho",
                ReviewNumber = "123"
            };
            reviews.Add(review);
            var mockReviewsRepo = new Mock<IReviewsRepository>();
            mockReviewsRepo.Setup(x => x.GetReviewByReviewNumber("123")).Returns(review);
            var mockReviewsVotesRepo = new Mock<IReviewsVotesRepository>();
            var reviewVote = new ReviewVote()
            {
                ReviewId = "test",
                ApplicationUserId = "gosho",
            };
            mockReviewsVotesRepo.Setup(x => x.GetReviewVoteByIds("test", "gosho")).Returns(reviewVote);

            var service = new ReviewsVotesService(mockReviewsVotesRepo.Object, mockReviewsRepo.Object);

            await service.UserReviewVoteAsync("123", true, "gosho");

            Assert.Equal("UpVote", reviewVote.Vote.ToString());

            await service.UserReviewVoteAsync("123", false, "gosho");

            Assert.Equal("DownVote", reviewVote.Vote.ToString());
        }

        [Fact]
        public async Task UserRevieVoteAsyncCreatesNewVoteCorrectly()
        {
            var reviews = new List<Review>();
            var reviewVotes = new List<ReviewVote>();
            var review = new Review()
            {
                Id = "test",
                ReviewAuthorId = "gosho",
                ReviewNumber = "123"
            };
            reviews.Add(review);
            var mockReviewsRepo = new Mock<IReviewsRepository>();
            mockReviewsRepo.Setup(x => x.GetReviewByReviewNumber("123")).Returns(review);
            var mockReviewsVotesRepo = new Mock<IReviewsVotesRepository>();
            mockReviewsVotesRepo.Setup(x => x.AddAsync(It.IsAny<ReviewVote>())).Callback((ReviewVote reviewVote) => reviewVotes.Add(reviewVote));

            var service = new ReviewsVotesService(mockReviewsVotesRepo.Object, mockReviewsRepo.Object);

            await service.UserReviewVoteAsync("123", true, "ivan");

            Assert.Single(reviewVotes);
            Assert.Equal("UpVote", reviewVotes.First().Vote.ToString());
            Assert.Equal("ivan", reviewVotes.First().ApplicationUserId);
            Assert.Equal("test", reviewVotes.First().ReviewId);

            await service.UserReviewVoteAsync("123", false, "pesho");

            var peshosVote = reviewVotes.ToArray()[1];
            Assert.Equal(2, reviewVotes.Count());
            Assert.Equal("DownVote", peshosVote.Vote.ToString());
            Assert.Equal("pesho", peshosVote.ApplicationUserId);
            Assert.Equal("test", peshosVote.ReviewId);
        }
    }
}
