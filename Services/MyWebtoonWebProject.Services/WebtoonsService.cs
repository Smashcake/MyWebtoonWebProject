﻿namespace MyWebtoonWebProject.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data;
    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public class WebtoonsService : IWebtoonsService
    {
        private readonly ApplicationDbContext dbContext;

        public WebtoonsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreateWebtoon(CreateWebtoonInputModel input)
        {
            string topFolder = @"c:\MyWebtoonWebProject\Webtoons";
            string webtoonFolder = Path.Combine(topFolder, input.Title);
            Directory.CreateDirectory(webtoonFolder);

            string coverPhotoPath = webtoonFolder + "/cover.png";
            using (FileStream fs = new FileStream(coverPhotoPath, FileMode.Create))
            {
                await input.Cover.CopyToAsync(fs);
            }

            var webtoonGenre = this.dbContext.Genres.First(g => g.Name == input.Genre);

            var webtoon = new Webtoon
            {
                Title = input.Title,
                Synopsis = input.Synopsis,
                CoverPhoto = coverPhotoPath,
                Genre = webtoonGenre,
                UploadDay = input.UploadDay,
                AuthorId = input.AuthorId,
                CreatedOn = DateTime.UtcNow,
                Rating = 0M,
                Completed = false,
            };

            this.dbContext.Webtoons.Add(webtoon);
            return await this.dbContext.SaveChangesAsync();
        }
    }
}