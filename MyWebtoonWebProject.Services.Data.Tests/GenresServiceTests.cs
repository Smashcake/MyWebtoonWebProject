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
    public class GenresServiceTests
    {
        [Fact]
        public async Task CreateGenreAsyncThrowsArgumentExceptionWhenGivenAExistingGenreName()
        {
            var genres = new List<Genre>();
            var mockGenreRepo = new Mock<IGenresRepository>();
            mockGenreRepo.Setup(x => x.GenreExist("gosho")).Returns(true);

            var service = new GenresService(mockGenreRepo.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateGenreAsync(new Web.ViewModels.Genres.CreateGenreInputModel() { Name = "gosho" }));
        }

        [Fact]
        public async Task CreateGenreAsyncCreatesNewGenreWhenNameIsNotTaken()
        {
            var genres = new List<Genre>();
            var mockGenreRepo = new Mock<IGenresRepository>();
            mockGenreRepo.Setup(x => x.GenreExist("gosho")).Returns(false);
            mockGenreRepo.Setup(x => x.AddAsync(It.IsAny<Genre>())).Callback((Genre genre) => genres.Add(genre));

            var service = new GenresService(mockGenreRepo.Object);

            await service.CreateGenreAsync(new Web.ViewModels.Genres.CreateGenreInputModel() { Name = "gosho" });

            Assert.Single(genres);
            Assert.Equal("gosho", genres.First().Name);
        }

        [Fact]
        public void GetAsKeyValuePairWorks()
        {
            var genres = new List<Genre>();
            var mockGenreRepo = new Mock<IGenresRepository>();
            mockGenreRepo.Setup(x => x.All()).Returns(genres.AsQueryable());
            genres.Add(new Genre()
            {
                Id = "test",
                Name = "123"
            });
            genres.Add(new Genre()
            {
                Id = "123",
                Name = "test"
            });

            var service = new GenresService(mockGenreRepo.Object);

            var result = service.GetAllAsKeyValuePairs();
            var secondGenre = result.ToArray()[1];

            Assert.Equal(2, result.Count());
            Assert.Equal("test", result.First().Key);
            Assert.Equal("123", result.First().Value);
            Assert.Equal("123", secondGenre.Key);
            Assert.Equal("test", secondGenre.Value);
        }

        [Fact]
        public void GetAllReturnsGenreModels()
        {
            var genres = new List<Genre>();
            var mockGenreRepo = new Mock<IGenresRepository>();
            mockGenreRepo.Setup(x => x.All()).Returns(genres.AsQueryable());
            genres.Add(new Genre()
            {
                Id = "test",
                Name = "123"
            });
            genres.Add(new Genre()
            {
                Id = "123",
                Name = "test"
            });

            var service = new GenresService(mockGenreRepo.Object);

            var result = service.AllGenres();
            var secondGenre = result.ToArray()[1];

            Assert.Equal(2, result.Count());
            Assert.Equal("test", result.First().Id);
            Assert.Equal("123", result.First().Name);
            Assert.Equal("123", secondGenre.Id);
            Assert.Equal("test", secondGenre.Name);
        }

        [Fact]
        public void DeleteGenreAsyncThrowsArgumentExceptionWhenGivenInvalidId()
        {
            var genres = new List<Genre>();
            var mockGenreRepo = new Mock<IGenresRepository>();
            mockGenreRepo.Setup(x => x.All()).Returns(genres.AsQueryable());

            var service = new GenresService(mockGenreRepo.Object);

            Assert.ThrowsAsync<ArgumentException>(() => service.DeleteGenre("ivan"));
        }

        [Fact]
        public async Task DeleteGenreAsyncWorksCorrectly()
        {
            var genres = new List<Genre>();
            var mockGenreRepo = new Mock<IGenresRepository>();
            mockGenreRepo.Setup(x => x.All()).Returns(genres.AsQueryable());
            mockGenreRepo.Setup(x => x.Delete(It.IsAny<Genre>())).Callback((Genre genre) => genres.Remove(genre));
            genres.Add(new Genre()
            {
                Id = "test123",
                Name = "ivan"
            });

            var service = new GenresService(mockGenreRepo.Object);

            await service.DeleteGenre("test123");

            Assert.Empty(genres);
        }

        [Fact]
        public void GetGenreNameWorksCorrectly()
        {
            var genres = new List<Genre>();
            var mockGenreRepo = new Mock<IGenresRepository>();
            mockGenreRepo.Setup(x => x.All()).Returns(genres.AsQueryable());
            genres.Add(new Genre()
            {
                Id = "test123",
                Name = "ivan"
            });

            var service = new GenresService(mockGenreRepo.Object);
            var result = service.GetGenreName("test123");

            Assert.Equal("ivan", result);
        }

        [Fact]
        public void EditGenreAsyncThrowsArgumentExceptionWhenGivenInvalidId()
        {
            var genres = new List<Genre>();
            var mockGenreRepo = new Mock<IGenresRepository>();
            mockGenreRepo.Setup(x => x.All()).Returns(genres.AsQueryable());

            var service = new GenresService(mockGenreRepo.Object);

            Assert.ThrowsAsync<ArgumentException>(() => service.EditGenre(new Web.ViewModels.Genres.EditGenreInputModel() { Id = "Ivan"}));
        }

        [Fact]
        public async Task EditGenreAsyncChangesNameCorrectly()
        {
            var genres = new List<Genre>();
            var mockGenreRepo = new Mock<IGenresRepository>();
            mockGenreRepo.Setup(x => x.All()).Returns(genres.AsQueryable());
            genres.Add(new Genre()
            {
                Id = "test123",
                Name = "ivan"
            });

            var service = new GenresService(mockGenreRepo.Object);

           await service.EditGenre(new Web.ViewModels.Genres.EditGenreInputModel() { Id = "test123" ,Name = "gosho"});

            Assert.Single(genres);
            Assert.Equal("gosho", genres.First().Name);
            Assert.Equal("test123", genres.First().Id);
        }
    }
}
