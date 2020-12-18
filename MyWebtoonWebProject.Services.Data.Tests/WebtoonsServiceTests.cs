using Microsoft.AspNetCore.Http;
using Moq;
using MyWebtoonWebProject.Data.Models;
using MyWebtoonWebProject.Data.Repositories;
using MyWebtoonWebProject.Web.ViewModels.Webtoons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyWebtoonWebProject.Services.Data.Tests
{
    public class WebtoonsServiceTests
    {
        [Fact]
        public void CreateWebtoonAsyncThrowsArgumentExceptionWhenAWebtoonWithTheSameTitleExists()
        {
            var webtoon = new Webtoon();
            var webtoons = new List<Webtoon>();
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            mockWebtoonsRepo.Setup(x => x.WebtoonExists(It.IsAny<string>())).Returns(true);

            var service = new WebtoonsService(mockWebtoonsRepo.Object, null, null, null, null, null, null, null, null, null);

            Assert.ThrowsAsync<ArgumentException>(() => service.CreateWebtoonAsync(null, null));
        }

        [Fact]
        public async Task CreateWebtoonAsyncWorksCorrectly()
        {
            var webtoons = new List<Webtoon>();
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            mockWebtoonsRepo.Setup(x => x.WebtoonExists(It.IsAny<string>())).Returns(false);
            mockWebtoonsRepo.Setup(x => x.GetWebtoonsCount()).Returns(0);
            mockWebtoonsRepo.Setup(x => x.AddAsync(It.IsAny<Webtoon>())).Callback((Webtoon webtoon) => webtoons.Add(webtoon));
            var mockIFormFile = new Mock<IFormFile>();
            var content = "Fake content";
            var fileName = "test.pdf";
            var contentType = ".exe";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            mockIFormFile.Setup(x => x.OpenReadStream()).Returns(ms);
            mockIFormFile.Setup(x => x.ContentType).Returns(contentType);
            mockIFormFile.Setup(x => x.FileName).Returns(fileName);
            mockIFormFile.Setup(x => x.Length).Returns(ms.Length);
            var input = new CreateWebtoonInputModel()
            {
                AuthorId = "pesho",
                Cover = mockIFormFile.Object,
                GenreId = "action",
                Synopsis = "this should work",
                Title = "Darkness",
                UploadDay = MyWebtoonWebProject.Data.Models.Enums.DayOfWeek.Friday,
            };
            var webRootPath = $@"C:\MyWebtoonWebProject\MyWebtoonWebProject\TestResults";

            var service = new WebtoonsService(mockWebtoonsRepo.Object, null, null, null, null, null, null, null, null, null);

            await service.CreateWebtoonAsync(input, webRootPath);

            Assert.Single(webtoons);
            Assert.Equal("pesho", webtoons.First().AuthorId);
            Assert.Equal("action", webtoons.First().GenreId);
            Assert.Equal("this should work", webtoons.First().Synopsis);
            Assert.Equal("Darkness", webtoons.First().Title);
            Assert.Equal("Friday", webtoons.First().UploadDay.ToString());
            Assert.Equal("Darkness/cover.pdf",webtoons.First().CoverPhoto);
        }

        [Fact]
        public void GetAllWebtoonsWorksCorrectly()
        {
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            var webtoons = new List<Webtoon>();
            var firstWebtoon = new Webtoon()
            {
                Id = "7",
                AuthorId = "123",
                GenreId = "14",
                Genre = new Genre()
                {
                    Name = "action",
                    Id = "14"
                },
                Author = new ApplicationUser()
                {
                    Id = "123",
                    UserName = "gosho"
                },
                Title = "Darkness",
                TitleNumber = "1",
                Episodes = new List<Episode>(),
            };
            var secondWebtoon = new Webtoon()
            {
                Id = "4",
                AuthorId = "456",
                GenreId = "15",
                Genre = new Genre()
                {
                    Name = "family-friendly",
                    Id = "15"
                },
                Author = new ApplicationUser()
                {
                    Id = "456",
                    UserName = "pesho"
                },
                Title = "Hello",
                TitleNumber = "2",
                Episodes = new List<Episode>(),
            };
            webtoons.Add(firstWebtoon);
            webtoons.Add(secondWebtoon);
            mockWebtoonsRepo.Setup(x => x.All()).Returns(webtoons.AsQueryable());
            var mockEpisodesRepo = new Mock<IEpisodesRepository>();
            mockEpisodesRepo.Setup(x => x.GetEpisodesByWebtoonId(It.IsAny<string>())).Returns((string webtoonId) => webtoons.FirstOrDefault(x => x.Id == webtoonId).Episodes);
            var mockGenresRepo = new Mock<IGenresRepository>();
            mockGenresRepo.Setup(x => x.GetGenreByWebtoonGenreId(It.IsAny<string>())).Returns((string genreId) => webtoons.FirstOrDefault(x => x.GenreId == genreId).Genre);
            var mockAppUserRepo = new Mock<IApplicationUserRepository>();
            mockAppUserRepo.Setup(x => x.GetWebtoonAuthorUsername(It.IsAny<string>())).Returns((string authorId) => webtoons.FirstOrDefault(x => x.AuthorId == authorId).Author.UserName);
            var mockEpisodeLikeService = new Mock<IEpisodesLikesService>();
            mockEpisodeLikeService.Setup(x => x.GetEpisodeLikes(It.IsAny<string>())).Returns(0);

            var service = new WebtoonsService(mockWebtoonsRepo.Object, mockEpisodesRepo.Object, mockGenresRepo.Object, mockAppUserRepo.Object, null, null, null, mockEpisodeLikeService.Object, null, null);

            var result = service.GetAllWebtoons();

            var firstResult = result.ToArray()[0];
            var secondResult = result.ToArray()[1];

            Assert.Equal(2, result.Count);
            Assert.Equal("action", firstResult.GenreName);
            Assert.Equal("family-friendly", secondResult.GenreName);
            Assert.Single(firstResult.Webtoons);
            Assert.Single(secondResult.Webtoons);
            Assert.Equal("gosho", firstResult.Webtoons.First().Author);
            Assert.Equal("pesho", secondResult.Webtoons.First().Author);
            Assert.Equal("Darkness", firstResult.Webtoons.First().Title);
            Assert.Equal("Hello", secondResult.Webtoons.First().Title);
            Assert.Equal("1", firstResult.Webtoons.First().TitleNumber);
            Assert.Equal("2", secondResult.Webtoons.First().TitleNumber);
            Assert.Equal(0, firstResult.Webtoons.First().Likes);
            Assert.Equal(0, secondResult.Webtoons.First().Likes);
            Assert.Empty(firstResult.Webtoons.First().Episodes);
            Assert.Empty(secondResult.Webtoons.First().Episodes);
        }

        [Fact]
        public void EditWebtoonAsyncThrowsArgumentExceptionWhenGivenInvalidAuthorId()
        {
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            var webtoons = new List<Webtoon>();
            var firstWebtoon = new Webtoon()
            {
                Id = "7",
                AuthorId = "123",
                GenreId = "14",
                Genre = new Genre()
                {
                    Name = "action",
                    Id = "14"
                },
                Author = new ApplicationUser()
                {
                    Id = "123",
                    UserName = "gosho"
                },
                Title = "Darkness",
                TitleNumber = "1",
                Episodes = new List<Episode>(),
            };
            mockWebtoonsRepo.Setup(x => x.GetWebtoonByTitleNumber(It.IsAny<string>())).Returns((string webtoonTitleNumber) => webtoons.FirstOrDefault(x => x.TitleNumber == webtoonTitleNumber));

            var service = new WebtoonsService(mockWebtoonsRepo.Object, null, null, null, null, null, null, null, null, null);

            Assert.ThrowsAsync<ArgumentException>(() => service.EditWebtoon(null,"456",null));
        }

        [Fact]
        public async Task EditWebtoonAsyncWorksCorrectly()
        {
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            var webtoons = new List<Webtoon>();
            var firstWebtoon = new Webtoon()
            {
                Id = "7",
                AuthorId = "123",
                GenreId = "14",
                Genre = new Genre()
                {
                    Name = "action",
                    Id = "14"
                },
                Author = new ApplicationUser()
                {
                    Id = "123",
                    UserName = "gosho"
                },
                Title = "New Title",
                TitleNumber = "1",
                UploadDay = MyWebtoonWebProject.Data.Models.Enums.DayOfWeek.Friday,
                Synopsis = "old synopsis",
                CoverPhoto = "my old friend.jpeg"
            };
            mockWebtoonsRepo.Setup(x => x.GetWebtoonByTitleNumber("1")).Returns(firstWebtoon);
            var input = new EditWebtoonInputModel()
            {
                Title = "Darkness",
                OldTitle = "Darkness",
                GenreId = "16",
                UploadDay = MyWebtoonWebProject.Data.Models.Enums.DayOfWeek.Tuesday,
                Synopsis = "new synopsis",
                WebtoonTitleNumber = "1",
            };
            var fakePath = $@"C:\MyWebtoonWebProject\MyWebtoonWebProject\TestResults";

            var service = new WebtoonsService(mockWebtoonsRepo.Object, null, null, null, null, null, null, null, null, null);

            await service.EditWebtoon(input, "123", fakePath);

            Assert.Equal("Darkness", firstWebtoon.Title);
            Assert.Equal("16", firstWebtoon.GenreId);
            Assert.Equal("Tuesday", firstWebtoon.UploadDay.ToString());
            Assert.Equal("new synopsis", firstWebtoon.Synopsis);
        }

        [Fact]
        public void GetWebtoonToEditWorksCorrectly()
        {
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            var webtoons = new List<Webtoon>();
            var firstWebtoon = new Webtoon()
            {
                Id = "7",
                AuthorId = "123",
                GenreId = "14",
                Genre = new Genre()
                {
                    Name = "action",
                    Id = "14"
                },
                Author = new ApplicationUser()
                {
                    Id = "123",
                    UserName = "gosho"
                },
                Title = "Darkness",
                TitleNumber = "1",
                UploadDay = MyWebtoonWebProject.Data.Models.Enums.DayOfWeek.Friday,
                Synopsis = "old synopsis",
                CoverPhoto = "my old friend.jpeg"
            };
            mockWebtoonsRepo.Setup(x => x.GetWebtoonByTitleNumber("1")).Returns(firstWebtoon);

            var service = new WebtoonsService(mockWebtoonsRepo.Object, null, null, null, null, null, null, null, null, null);

            var result = service.GetWebtoonToEdit("1", "123");

            Assert.Equal("Darkness", result.OldTitle);
            Assert.Equal("1", result.WebtoonTitleNumber);
            Assert.Equal("old synopsis", result.Synopsis);
            Assert.Equal("Friday", result.UploadDay.ToString());
        }

        [Fact]
        public async Task DeleteWebtoonAsyncThrowsArgumentExceptionWhenGivenInvalidAuthorId()
        {
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            var webtoons = new List<Webtoon>();
            var firstWebtoon = new Webtoon()
            {
                Id = "7",
                AuthorId = "123",
                GenreId = "14",
                Genre = new Genre()
                {
                    Name = "action",
                    Id = "14"
                },
                Author = new ApplicationUser()
                {
                    Id = "123",
                    UserName = "gosho"
                },
                Title = "Darkness",
                TitleNumber = "1",
                UploadDay = MyWebtoonWebProject.Data.Models.Enums.DayOfWeek.Friday,
                Synopsis = "old synopsis",
                CoverPhoto = "my old friend.jpeg"
            };
            mockWebtoonsRepo.Setup(x => x.GetWebtoonByTitleNumber("1")).Returns(firstWebtoon);

            var service = new WebtoonsService(mockWebtoonsRepo.Object, null, null, null, null, null, null, null, null, null);


            await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteWebtoonAsync("1","456"));
        }

        [Fact]
        public async Task DeleteWebtoonAsyncWorksCorrectly()
        {
            var mockWebtoonsRepo = new Mock<IWebtoonsRepository>();
            var webtoons = new List<Webtoon>();
            var firstWebtoon = new Webtoon()
            {
                Id = "7",
                AuthorId = "123",
                GenreId = "14",
                Genre = new Genre()
                {
                    Name = "action",
                    Id = "14"
                },
                Author = new ApplicationUser()
                {
                    Id = "123",
                    UserName = "gosho"
                },
                Title = "Darkness",
                TitleNumber = "1",
                UploadDay = MyWebtoonWebProject.Data.Models.Enums.DayOfWeek.Friday,
                Synopsis = "old synopsis",
                CoverPhoto = "my old friend.jpeg"
            };
            webtoons.Add(firstWebtoon);
            mockWebtoonsRepo.Setup(x => x.GetWebtoonByTitleNumber("1")).Returns(firstWebtoon);
            mockWebtoonsRepo.Setup(x => x.Delete(It.IsAny<Webtoon>())).Callback((Webtoon webtoon) => webtoon.IsDeleted = true);

            var service = new WebtoonsService(mockWebtoonsRepo.Object, null, null, null, null, null, null, null, null, null);

            await service.DeleteWebtoonAsync("1","123");

            Assert.Single(webtoons);
            Assert.True(firstWebtoon.IsDeleted);
        }
    }
}
