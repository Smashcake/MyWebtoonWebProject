using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using MyWebtoonWebProject.Data.Models;
using MyWebtoonWebProject.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyWebtoonWebProject.Services.Data.Tests
{
    public class PagesServiceTests
    {
        [Fact]
        public async Task AddPagesAsyncThrowsArgumentErrorWhenGivenAnInvalidFileFormat()
        {
            var pages = new List<Page>();
            var mockPagesRepo = new Mock<IPagesRepository>();
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
            var formFilePages = new List<IFormFile>();
            formFilePages.Add(mockIFormFile.Object);
            var service = new PagesService(mockPagesRepo.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => service.AddPagesAsync(formFilePages, "hello", "sadness"));
        }

        [Fact]
        public async Task AddPagesAsyncThrowsArgumentErrorWhenGivenAnInvalidFileSize()
        {
            var pages = new List<Page>();
            var mockPagesRepo = new Mock<IPagesRepository>();
            var mockIFormFile = new Mock<IFormFile>();
            var content = "Fake content";
            var fileName = "test.jpeg";
            var contentType = ".jpeg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            mockIFormFile.Setup(x => x.OpenReadStream()).Returns(ms);
            mockIFormFile.Setup(x => x.ContentType).Returns(contentType);
            mockIFormFile.Setup(x => x.FileName).Returns(fileName);
            mockIFormFile.Setup(x => x.Length).Returns(ms.Length*1024*1024*1024);
            var formFilePages = new List<IFormFile>();
            formFilePages.Add(mockIFormFile.Object);
            var service = new PagesService(mockPagesRepo.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => service.AddPagesAsync(formFilePages, "hello", "sadness"));
        }

        [Fact]
        public async Task AddPagesAsyncWorksCorrectlyWithJpeg()
        {
            var pages = new List<Page>();
            var mockPagesRepo = new Mock<IPagesRepository>();
            var mockIFormFile = new Mock<IFormFile>();
            var content = "Fake content";
            var fileName = "test.jpeg";
            var contentType = ".jpeg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            mockIFormFile.Setup(x => x.OpenReadStream()).Returns(ms);
            mockIFormFile.Setup(x => x.ContentType).Returns(contentType);
            mockIFormFile.Setup(x => x.FileName).Returns(fileName);
            mockIFormFile.Setup(x => x.Length).Returns(ms.Length);
            mockPagesRepo.Setup(x => x.AddAsync(It.IsAny<Page>())).Callback((Page page) => pages.Add(page));
            var formFilePages = new List<IFormFile>();
            formFilePages.Add(mockIFormFile.Object);
            var service = new PagesService(mockPagesRepo.Object);

            // Had to make a specific folder for this to go through
            var result = await service.AddPagesAsync(formFilePages, @"C:\Users\Smashcake\Desktop\forUnitTest", "test");

            Assert.Single(pages);
            Assert.Single(result);
            Assert.Equal("test", pages.First().EpisodeId);
            Assert.Equal("test", result.First().EpisodeId);
            Assert.Equal(1, pages.First().PageNumber);
            Assert.Equal(1, result.First().PageNumber);
            Assert.Equal("jpeg", pages.First().FileExtention);
            Assert.Equal("jpeg", result.First().FileExtention);
            Assert.Equal("/page1.jpeg", pages.First().FilePath);
            Assert.Equal("/page1.jpeg", result.First().FilePath);
        }

        [Fact]
        public async Task AddPagesAsyncWorksCorrectlyWithJpg()
        {
            var pages = new List<Page>();
            var mockPagesRepo = new Mock<IPagesRepository>();
            var mockIFormFile = new Mock<IFormFile>();
            var content = "Fake content";
            var fileName = "test.jpg";
            var contentType = ".jpg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            mockIFormFile.Setup(x => x.OpenReadStream()).Returns(ms);
            mockIFormFile.Setup(x => x.ContentType).Returns(contentType);
            mockIFormFile.Setup(x => x.FileName).Returns(fileName);
            mockIFormFile.Setup(x => x.Length).Returns(ms.Length);
            mockPagesRepo.Setup(x => x.AddAsync(It.IsAny<Page>())).Callback((Page page) => pages.Add(page));
            var formFilePages = new List<IFormFile>();
            formFilePages.Add(mockIFormFile.Object);
            var service = new PagesService(mockPagesRepo.Object);

            // Had to make a specific folder for this to go through
            var result = await service.AddPagesAsync(formFilePages, @"C:\Users\Smashcake\Desktop\forUnitTest", "test");

            Assert.Single(pages);
            Assert.Single(result);
            Assert.Equal("test", pages.First().EpisodeId);
            Assert.Equal("test", result.First().EpisodeId);
            Assert.Equal(1, pages.First().PageNumber);
            Assert.Equal(1, result.First().PageNumber);
            Assert.Equal("jpg", pages.First().FileExtention);
            Assert.Equal("jpg", result.First().FileExtention);
            Assert.Equal("/page1.jpg", pages.First().FilePath);
            Assert.Equal("/page1.jpg", result.First().FilePath);
        }

        [Fact]
        public async Task AddPagesAsyncWorksCorrectlyWithPng()
        {
            var pages = new List<Page>();
            var mockPagesRepo = new Mock<IPagesRepository>();
            var mockIFormFile = new Mock<IFormFile>();
            var content = "Fake content";
            var fileName = "test.png";
            var contentType = ".png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            mockIFormFile.Setup(x => x.OpenReadStream()).Returns(ms);
            mockIFormFile.Setup(x => x.ContentType).Returns(contentType);
            mockIFormFile.Setup(x => x.FileName).Returns(fileName);
            mockIFormFile.Setup(x => x.Length).Returns(ms.Length);
            mockPagesRepo.Setup(x => x.AddAsync(It.IsAny<Page>())).Callback((Page page) => pages.Add(page));
            var formFilePages = new List<IFormFile>();
            formFilePages.Add(mockIFormFile.Object);
            var service = new PagesService(mockPagesRepo.Object);

            // Had to make a specific folder for this to go through
            var result = await service.AddPagesAsync(formFilePages, @"C:\Users\Smashcake\Desktop\forUnitTest", "test");

            Assert.Single(pages);
            Assert.Single(result);
            Assert.Equal("test", pages.First().EpisodeId);
            Assert.Equal("test", result.First().EpisodeId);
            Assert.Equal(1, pages.First().PageNumber);
            Assert.Equal(1, result.First().PageNumber);
            Assert.Equal("png", pages.First().FileExtention);
            Assert.Equal("png", result.First().FileExtention);
            Assert.Equal("/page1.png", pages.First().FilePath);
            Assert.Equal("/page1.png", result.First().FilePath);
        }
    }
}
