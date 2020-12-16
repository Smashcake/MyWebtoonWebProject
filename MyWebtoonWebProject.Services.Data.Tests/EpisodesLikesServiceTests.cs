namespace MyWebtoonWebProject.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using Xunit;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Repositories;

    public class EpisodesLikesServiceTests
    {
        [Fact]
        public void GetEpisodeLikesWorksCorrectly()
        {
            var episodeLikes = new List<EpisodeLike>();
            var mockEpisodeLikeRepo = new Mock<IEpisodesLikesRepository>();
            mockEpisodeLikeRepo.Setup(x => x.All()).Returns(episodeLikes.AsQueryable());

            var episode = new Episode()
            {
                Id = "test123",
            };

            var episodeLike = new EpisodeLike()
            {
                EpisodeId = "test123",
                ApplicationUserId = "pesho",
                HasLiked = true,
            };

            episode.EpisodeLikes.Add(episodeLike);
            episodeLikes.Add(episodeLike);

            var service = new EpisodesLikesService(mockEpisodeLikeRepo.Object);

            var result = service.GetEpisodeLikes("test123");
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task UserLikeAsyncSetsNewLikeValue()
        {
            var episodeLikes = new List<EpisodeLike>();
            var mockEpisodeLikeRepo = new Mock<IEpisodesLikesRepository>();
            mockEpisodeLikeRepo.Setup(x => x.All()).Returns(episodeLikes.AsQueryable());
            mockEpisodeLikeRepo.Setup(x => x.AddAsync(It.IsAny<EpisodeLike>())).Callback((EpisodeLike episodeLike) => episodeLikes.Add(episodeLike));

            var episode = new Episode()
            {
                Id = "test123",
            };

            var service = new EpisodesLikesService(mockEpisodeLikeRepo.Object);

            await service.UserLikeAsync("test123", "pesho");


            Assert.Single(episodeLikes);
            Assert.Equal("test123", episodeLikes.First().EpisodeId);
            Assert.Equal("pesho", episodeLikes.First().ApplicationUserId);
            Assert.True(episodeLikes.First().HasLiked);
        }

        [Fact]
        public async Task UserLikeAsyncChangesHasLikedWhenGivenAnExistingLike()
        {
            var episodeLikes = new List<EpisodeLike>();
            var mockEpisodeLikeRepo = new Mock<IEpisodesLikesRepository>();
            mockEpisodeLikeRepo.Setup(x => x.All()).Returns(episodeLikes.AsQueryable());
            mockEpisodeLikeRepo.Setup(x => x.AddAsync(It.IsAny<EpisodeLike>())).Callback((EpisodeLike episodeLike) => episodeLikes.Add(episodeLike));

            var episode = new Episode()
            {
                Id = "test123",
            };

            episodeLikes.Add(new EpisodeLike()
            {
                EpisodeId = "test123",
                ApplicationUserId = "pesho",
                HasLiked = true,
            });

            var service = new EpisodesLikesService(mockEpisodeLikeRepo.Object);

            await service.UserLikeAsync("test123", "pesho");


            Assert.Single(episodeLikes);
            Assert.Equal("test123", episodeLikes.First().EpisodeId);
            Assert.Equal("pesho", episodeLikes.First().ApplicationUserId);
            Assert.False(episodeLikes.First().HasLiked);

            await service.UserLikeAsync("test123", "pesho");

            Assert.Single(episodeLikes);
            Assert.Equal("test123", episodeLikes.First().EpisodeId);
            Assert.Equal("pesho", episodeLikes.First().ApplicationUserId);
            Assert.True(episodeLikes.First().HasLiked);
        }
    }
}
