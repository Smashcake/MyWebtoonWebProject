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
    public class WebtoonsRatingsServiceTests
    {
        [Fact]
        public async Task RateWebtoonAsyncCreatesNewRatingCorrectly()
        {
            var webtoons = new List<Webtoon>();
            var webtoon = new Webtoon()
            {
                Id = "test",
                TitleNumber = "123"
            };
            webtoons.Add(webtoon);
            var mockWebtoonRepo = new Mock<IWebtoonsRepository>();
            mockWebtoonRepo.Setup(x => x.GetWebtoonByTitleNumber("123")).Returns(webtoon);
            var webtoonRatings = new List<WebtoonRating>();
            var mockWebtoonRatingsRepo = new Mock<IWebtoonsRatingsRepository>();
            mockWebtoonRatingsRepo.Setup(x => x.All()).Returns(webtoonRatings.AsQueryable());
            mockWebtoonRatingsRepo.Setup(x => x.AddAsync(It.IsAny<WebtoonRating>())).Callback((WebtoonRating webtoonRating) => webtoonRatings.Add(webtoonRating));

            var service = new WebtoonsRatingsService(mockWebtoonRatingsRepo.Object, mockWebtoonRepo.Object);

            await service.RateWebtoonAsync("123", "gosho", 5);

            Assert.Single(webtoonRatings);
            Assert.Equal("test", webtoonRatings.First().WebtoonId);
            Assert.Equal("gosho", webtoonRatings.First().ApplicationUserId);
            Assert.Equal(5, webtoonRatings.First().RatingValue);
        }

        [Fact]
        public async Task RateWebtoonAsyncChangesRatingCorrectly()
        {
            var webtoons = new List<Webtoon>();
            var webtoon = new Webtoon()
            {
                Id = "test",
                TitleNumber = "123"
            };
            webtoons.Add(webtoon);
            var mockWebtoonRepo = new Mock<IWebtoonsRepository>();
            mockWebtoonRepo.Setup(x => x.GetWebtoonByTitleNumber("123")).Returns(webtoon);
            var webtoonRatings = new List<WebtoonRating>();
            webtoonRatings.Add(new WebtoonRating()
            {
                ApplicationUserId = "gosho",
                WebtoonId = "test",
                RatingValue = 1,
            });
            var mockWebtoonRatingsRepo = new Mock<IWebtoonsRatingsRepository>();
            mockWebtoonRatingsRepo.Setup(x => x.All()).Returns(webtoonRatings.AsQueryable());

            var service = new WebtoonsRatingsService(mockWebtoonRatingsRepo.Object, mockWebtoonRepo.Object);

            await service.RateWebtoonAsync("123", "gosho", 4);

            Assert.Single(webtoonRatings);
            Assert.Equal("test", webtoonRatings.First().WebtoonId);
            Assert.Equal("gosho", webtoonRatings.First().ApplicationUserId);
            Assert.Equal(4, webtoonRatings.First().RatingValue);
        }

        [Fact]
        public async Task GetWebtoonAverageRatingWorksCorrectly()
        {
            var webtoons = new List<Webtoon>();
            var webtoon = new Webtoon()
            {
                Id = "test",
                TitleNumber = "123"
            };
            webtoons.Add(webtoon);
            var mockWebtoonRepo = new Mock<IWebtoonsRepository>();
            mockWebtoonRepo.Setup(x => x.GetWebtoonByTitleNumber("123")).Returns(webtoon);
            var webtoonRatings = new List<WebtoonRating>();
            webtoonRatings.Add(new WebtoonRating()
            {
                ApplicationUserId = "gosho",
                WebtoonId = "test",
                RatingValue = 1,
            });
            var mockWebtoonRatingsRepo = new Mock<IWebtoonsRatingsRepository>();
            mockWebtoonRatingsRepo.Setup(x => x.All()).Returns(webtoonRatings.AsQueryable());
            mockWebtoonRatingsRepo.Setup(x => x.AddAsync(It.IsAny<WebtoonRating>())).Callback((WebtoonRating webtoonRating) => webtoonRatings.Add(webtoonRating));

            var service = new WebtoonsRatingsService(mockWebtoonRatingsRepo.Object, mockWebtoonRepo.Object);

            await service.RateWebtoonAsync("123", "ivan", 4);

            Assert.Equal(2,webtoonRatings.Count());
            Assert.Equal(2.5, service.GetWebtoonAverageRating("123"));
        }

        [Fact]
        public async Task DoesWebtoonHaveARatingCorrectlyReturnsFalseWhenNoRatingsArePresent()
        {
            var webtoons = new List<Webtoon>();
            var webtoon = new Webtoon()
            {
                Id = "test",
                TitleNumber = "123"
            };
            webtoons.Add(webtoon);
            var mockWebtoonRepo = new Mock<IWebtoonsRepository>();
            mockWebtoonRepo.Setup(x => x.GetWebtoonByTitleNumber("123")).Returns(webtoon);
            var webtoonRatings = new List<WebtoonRating>();
            var mockWebtoonRatingsRepo = new Mock<IWebtoonsRatingsRepository>();
            mockWebtoonRatingsRepo.Setup(x => x.All()).Returns(webtoonRatings.AsQueryable());

            var service = new WebtoonsRatingsService(mockWebtoonRatingsRepo.Object, mockWebtoonRepo.Object);

            var result = service.DoesWebtoonHaveARating("123");

            Assert.False(result);
        }

        [Fact]
        public async Task DoesWebtoonHaveARatingCorrectlyReturnsTrueWhenVotesArePresent()
        {
            var webtoons = new List<Webtoon>();
            var webtoon = new Webtoon()
            {
                Id = "test",
                TitleNumber = "123"
            };
            webtoons.Add(webtoon);
            var mockWebtoonRepo = new Mock<IWebtoonsRepository>();
            mockWebtoonRepo.Setup(x => x.GetWebtoonByTitleNumber("123")).Returns(webtoon);
            var webtoonRatings = new List<WebtoonRating>();
            webtoonRatings.Add(new WebtoonRating()
            {
                ApplicationUserId = "gosho",
                WebtoonId = "test",
                RatingValue = 1,
            });
            var mockWebtoonRatingsRepo = new Mock<IWebtoonsRatingsRepository>();
            mockWebtoonRatingsRepo.Setup(x => x.All()).Returns(webtoonRatings.AsQueryable());
            mockWebtoonRatingsRepo.Setup(x => x.AddAsync(It.IsAny<WebtoonRating>())).Callback((WebtoonRating webtoonRating) => webtoonRatings.Add(webtoonRating));

            var service = new WebtoonsRatingsService(mockWebtoonRatingsRepo.Object, mockWebtoonRepo.Object);

            await service.RateWebtoonAsync("123", "ivan", 4);

            var result = service.DoesWebtoonHaveARating("123");

            Assert.True(result);
        }
    }
}
