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
    public class WebtoonsSubscribersServiceTests
    {
        [Fact]
        public async Task SubscribeUserToWebtoonCorrectlySubscribes()
        {
            var webtoonsSubscribers = new List<WebtoonsSubscribers>();
            var mockWebtoonSubsRepo = new Mock<IWebtoonsSubscribersRepository>();
            mockWebtoonSubsRepo.Setup(x => x.All()).Returns(webtoonsSubscribers.AsQueryable());
            mockWebtoonSubsRepo.Setup(x => x.AddAsync(It.IsAny<WebtoonsSubscribers>())).Callback((WebtoonsSubscribers webtoonSubscriber) => webtoonsSubscribers.Add(webtoonSubscriber));

            var service = new WebtoonsSubscribersService(mockWebtoonSubsRepo.Object);

            await service.SubscribeUserToWebtoonAsync("test", "gosho");

            Assert.Single(webtoonsSubscribers);
            Assert.Equal("test", webtoonsSubscribers.First().WebtoonId);
            Assert.Equal("gosho", webtoonsSubscribers.First().SubscriberId);
        }

        [Fact]
        public async Task SubscribeUserToWebtoonCorrectlyRemovesSubscribtion()
        {
            var webtoonsSubscribers = new List<WebtoonsSubscribers>();
            var mockWebtoonSubsRepo = new Mock<IWebtoonsSubscribersRepository>();
            mockWebtoonSubsRepo.Setup(x => x.All()).Returns(webtoonsSubscribers.AsQueryable());
            mockWebtoonSubsRepo.Setup(x => x.AddAsync(It.IsAny<WebtoonsSubscribers>())).Callback((WebtoonsSubscribers webtoonSubscriber) => webtoonsSubscribers.Add(webtoonSubscriber));
            mockWebtoonSubsRepo.Setup(x => x.Delete(It.IsAny<WebtoonsSubscribers>())).Callback((WebtoonsSubscribers webtoonSubscriber) => webtoonsSubscribers.Remove(webtoonSubscriber));

            var service = new WebtoonsSubscribersService(mockWebtoonSubsRepo.Object);

            await service.SubscribeUserToWebtoonAsync("test", "gosho");

            Assert.Single(webtoonsSubscribers);
            Assert.Equal("test", webtoonsSubscribers.First().WebtoonId);
            Assert.Equal("gosho", webtoonsSubscribers.First().SubscriberId);

            await service.SubscribeUserToWebtoonAsync("test", "gosho");
            Assert.Empty(webtoonsSubscribers);
        }
    }
}
