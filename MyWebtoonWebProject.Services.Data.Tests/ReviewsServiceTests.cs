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
    public class ReviewsServiceTests
    {
        [Fact]
        public async Task AddReviewAsyncWorksCorrectly()
        {
            var webtoons = new List<Webtoon>();
            webtoons.Add(new Webtoon
            {
                Id = "test",
                TitleNumber = "123"
            });
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            mockWebtoonsRepo.Setup(x => x.GetWebtoonByTitleNumber("123")).Returns(webtoons.First());
            var reviews = new List<Review>();
            var mockReviewsRepo = new Mock<IReviewsRepository>();
            mockReviewsRepo.Setup(x => x.All()).Returns(reviews.AsQueryable());
            mockReviewsRepo.Setup(x => x.AddAsync(It.IsAny<Review>())).Callback((Review review) => reviews.Add(review));

            var service = new ReviewsService(mockReviewsRepo.Object, mockWebtoonsRepo.Object);

            await service.AddReviewAsync(new Web.ViewModels.Reviews.LeaveReviewInputModel()
            {
                UserId = "gosho",
                UserReview = "This should work",
                WebtoonTitleNumber = "123"
            });

            Assert.Single(reviews);
            Assert.Equal("gosho", reviews.First().ReviewAuthorId);
            Assert.Equal("This should work", reviews.First().ReviewInfo);
        }

        [Fact]
        public void AddReviewAsyncThrowsArgumentExceptionWhenUserReviewExists()
        {
            var webtoons = new List<Webtoon>();
            webtoons.Add(new Webtoon
            {
                Id = "test",
                TitleNumber = "123"
            });
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            mockWebtoonsRepo.Setup(x => x.GetWebtoonByTitleNumber("123")).Returns(webtoons.First());
            var reviews = new List<Review>();
            var mockReviewsRepo = new Mock<IReviewsRepository>();
            mockReviewsRepo.Setup(x => x.All()).Returns(reviews.AsQueryable());
            reviews.Add(new Review()
            {
                ReviewAuthorId = "gosho",
                WebtoonId = "test"
            });

            var service = new ReviewsService(mockReviewsRepo.Object, mockWebtoonsRepo.Object);

            Assert.ThrowsAsync<ArgumentException>(() => service.AddReviewAsync(new Web.ViewModels.Reviews.LeaveReviewInputModel()
            {
                UserId = "gosho",
                WebtoonTitleNumber = "123"
            }));
        }

        [Fact]
        public void ReviewLikesAndDislikesWorksCorrectly()
        {           
            var reviews = new List<Review>();
            var review = new Review()
            {
                ReviewNumber = "pesho",
                Id = "test123"
            };
            reviews.Add(review);
            var reviewVote = new ReviewVote()
            {
                ApplicationUserId = "gosho",
                ReviewId = "test123",
                Vote = MyWebtoonWebProject.Data.Models.Enums.VoteType.UpVote
            };
            var secondReviewVote = new ReviewVote()
            {
                ApplicationUserId = "pesho",
                ReviewId = "test123",
                Vote = MyWebtoonWebProject.Data.Models.Enums.VoteType.DownVote
            };
            review.ReviewVotes.Add(reviewVote);
            review.ReviewVotes.Add(secondReviewVote);
            var mockReviewsRepo = new Mock<IReviewsRepository>();;
            mockReviewsRepo.Setup(x => x.GetReviewByReviewNumber("pesho")).Returns(review);

            var service = new ReviewsService(mockReviewsRepo.Object, null);

            var result = service.ReviewLikesAndDislikes("pesho");

            Assert.Equal(1,result.Dislikes);
            Assert.Equal(1,result.Likes);
        }

        [Fact]
        public async Task DeleteReviewAsyncThrowsArgumentExceptionWhenGivenInvalidAuthorId()
        {
            var reviews = new List<Review>();
            var review = new Review()
            {
                ReviewNumber = "pesho",
                Id = "test123",
                ReviewAuthorId = "gosho"
            };
            reviews.Add(review);
            var mockReviewsRepo = new Mock<IReviewsRepository>(); ;
            mockReviewsRepo.Setup(x => x.GetReviewByReviewNumber("pesho")).Returns(review);

            var service = new ReviewsService(mockReviewsRepo.Object, null);

            await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteReviewAsync("pesho", "ivan"));
        }

        [Fact]
        public async Task DeleteReviewAsyncWorksCorrectly()
        {
            var reviews = new List<Review>();
            var review = new Review()
            {
                ReviewNumber = "pesho",
                Id = "test123",
                ReviewAuthorId = "gosho"
            };
            reviews.Add(review);
            var mockReviewsRepo = new Mock<IReviewsRepository>(); ;
            mockReviewsRepo.Setup(x => x.GetReviewByReviewNumber("pesho")).Returns(review);
            mockReviewsRepo.Setup(x => x.Delete(It.IsAny<Review>())).Callback((Review review) => review.IsDeleted = true);
            var service = new ReviewsService(mockReviewsRepo.Object, null);

            await service.DeleteReviewAsync("pesho", "gosho");

            Assert.Single(reviews);
            Assert.True(reviews.First().IsDeleted);
        }

        [Fact]
        public void EditReviewAsyncThrowsArgumentExceptionWhenGivenInvalidReviewAuthorId()
        {
            var reviews = new List<Review>();
            var review = new Review()
            {
                ReviewNumber = "pesho",
                Id = "test123",
                ReviewAuthorId = "gosho"
            };
            reviews.Add(review);
            var mockReviewsRepo = new Mock<IReviewsRepository>(); ;
            mockReviewsRepo.Setup(x => x.GetReviewByReviewNumber("pesho")).Returns(review);

            var service = new ReviewsService(mockReviewsRepo.Object, null);

            Assert.ThrowsAsync<ArgumentException>(() => service.EditReviewAsync("pesho", "ivan", "blank"));
        }

        [Fact]
        public async Task EditReviewAsyncWorksCorrectly()
        {
            var reviews = new List<Review>();
            var review = new Review()
            {
                ReviewNumber = "pesho",
                Id = "test123",
                ReviewAuthorId = "gosho"
            };
            reviews.Add(review);
            var mockReviewsRepo = new Mock<IReviewsRepository>(); ;
            mockReviewsRepo.Setup(x => x.GetReviewByReviewNumber("pesho")).Returns(review);

            var service = new ReviewsService(mockReviewsRepo.Object, null);

            await service.EditReviewAsync("pesho", "gosho", "blank");

            Assert.Single(reviews);
            Assert.Equal("blank", reviews.First().ReviewInfo);
        }
    }
}
