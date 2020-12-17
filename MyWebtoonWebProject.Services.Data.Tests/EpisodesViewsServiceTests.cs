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
    public class EpisodesViewsServiceTests
    {
        [Fact]
        public async Task UserEpisodeViewCreatesNewViewObjectWhenUserHasNotViewedBefore()
        {
            var episodes = new List<Episode>();
            var episode = new Episode()
            {
                Webtoon = new Webtoon()
                {
                    TitleNumber = "123",
                    Id = "test"
                },
                EpisodeNumber = "pesho",
                Id = "gosho"
            };
            var mockEpisodesService = new Mock<IEpisodesService>();
            mockEpisodesService.Setup(x => x.GetEpisodeId(episode.Webtoon.TitleNumber, episode.EpisodeNumber)).Returns(episode.Id);

            var episodeViews = new List<EpisodeView>();
            var mockEpisodeViewsRepo = new Mock<IEpisodesViewsRepository>();
            mockEpisodeViewsRepo.Setup(x => x.All()).Returns(episodeViews.AsQueryable());
            mockEpisodeViewsRepo.Setup(x => x.AddAsync(It.IsAny<EpisodeView>())).Callback((EpisodeView episodeView) => episodeViews.Add(episodeView));

            var service = new EpisodesViewsService(mockEpisodeViewsRepo.Object, mockEpisodesService.Object);

            await service.UserEpisodeView("123", "pesho", "ivan");

            Assert.Single(episodeViews);
            Assert.Equal("ivan", episodeViews.First().ApplicationUserId);
            Assert.Equal("gosho", episodeViews.First().EpisodeId);
            Assert.Equal(1, episodeViews.First().ViewCount);
        }

        [Fact]
        public async Task UserEpisodeViewUpdatesViewCountWhenAUserHasLastViewedItInMoreThanADay()
        {
            var episodes = new List<Episode>();
            var episode = new Episode()
            {
                Webtoon = new Webtoon()
                {
                    TitleNumber = "123",
                    Id = "test"
                },
                EpisodeNumber = "pesho",
                Id = "gosho"
            };
            var mockEpisodesService = new Mock<IEpisodesService>();
            mockEpisodesService.Setup(x => x.GetEpisodeId(episode.Webtoon.TitleNumber, episode.EpisodeNumber)).Returns(episode.Id);

            var episodeViews = new List<EpisodeView>();
            episodeViews.Add(new EpisodeView()
            {
                EpisodeId = "gosho",
                ApplicationUserId = "ivan",
                ViewCount = 4,
                LastViewedOn = DateTime.UtcNow.Subtract(new TimeSpan(48, 48, 48)),
            });
            var mockEpisodeViewsRepo = new Mock<IEpisodesViewsRepository>();
            mockEpisodeViewsRepo.Setup(x => x.All()).Returns(episodeViews.AsQueryable());
            mockEpisodeViewsRepo.Setup(x => x.AddAsync(It.IsAny<EpisodeView>())).Callback((EpisodeView episodeView) => episodeViews.Add(episodeView));

            var service = new EpisodesViewsService(mockEpisodeViewsRepo.Object, mockEpisodesService.Object);

            await service.UserEpisodeView("123", "pesho", "ivan");

            Assert.Single(episodeViews);
            Assert.Equal("ivan", episodeViews.First().ApplicationUserId);
            Assert.Equal("gosho", episodeViews.First().EpisodeId);
            Assert.Equal(5, episodeViews.First().ViewCount);
        }

        [Fact]
        public async Task UserEpisodeViewDoesNotUpdateViewCountWhenUserHasViewedEpisodeInTheLast24Hours()
        {
            var episodes = new List<Episode>();
            var episode = new Episode()
            {
                Webtoon = new Webtoon()
                {
                    TitleNumber = "123",
                    Id = "test"
                },
                EpisodeNumber = "pesho",
                Id = "gosho"
            };
            var mockEpisodesService = new Mock<IEpisodesService>();
            mockEpisodesService.Setup(x => x.GetEpisodeId(episode.Webtoon.TitleNumber, episode.EpisodeNumber)).Returns(episode.Id);

            var episodeViews = new List<EpisodeView>();
            episodeViews.Add(new EpisodeView()
            {
                EpisodeId = "gosho",
                ApplicationUserId = "ivan",
                ViewCount = 4,
                LastViewedOn = DateTime.UtcNow,
            });
            var mockEpisodeViewsRepo = new Mock<IEpisodesViewsRepository>();
            mockEpisodeViewsRepo.Setup(x => x.All()).Returns(episodeViews.AsQueryable());
            mockEpisodeViewsRepo.Setup(x => x.AddAsync(It.IsAny<EpisodeView>())).Callback((EpisodeView episodeView) => episodeViews.Add(episodeView));

            var service = new EpisodesViewsService(mockEpisodeViewsRepo.Object, mockEpisodesService.Object);

            await service.UserEpisodeView("123", "pesho", "ivan");

            Assert.Single(episodeViews);
            Assert.Equal("ivan", episodeViews.First().ApplicationUserId);
            Assert.Equal("gosho", episodeViews.First().EpisodeId);
            Assert.Equal(4, episodeViews.First().ViewCount);
        }

        [Fact]
        public void EpisodeTotalViewsReturnsProperValue()
        {
            var episodes = new List<Episode>();
            var episode = new Episode()
            {
                Webtoon = new Webtoon()
                {
                    TitleNumber = "123",
                    Id = "test"
                },
                EpisodeNumber = "pesho",
                Id = "gosho"
            };
            var mockEpisodesService = new Mock<IEpisodesService>();
            mockEpisodesService.Setup(x => x.GetEpisodeId(episode.Webtoon.TitleNumber, episode.EpisodeNumber)).Returns(episode.Id);

            var episodeViews = new List<EpisodeView>();
            episodeViews.Add(new EpisodeView()
            {
                EpisodeId = "gosho",
                ApplicationUserId = "ivan",
                ViewCount = 4,
                LastViewedOn = DateTime.UtcNow,
            });
            var mockEpisodeViewsRepo = new Mock<IEpisodesViewsRepository>();
            mockEpisodeViewsRepo.Setup(x => x.All()).Returns(episodeViews.AsQueryable());

            var service = new EpisodesViewsService(mockEpisodeViewsRepo.Object, mockEpisodesService.Object);

            Assert.Equal(4, service.EpisodeTotalViews("123", "pesho"));
        }
    }
}
