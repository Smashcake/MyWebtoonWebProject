namespace MyWebtoonWebProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyWebtoonWebProject.Data;
    using MyWebtoonWebProject.Data.Models;

    public class PagesService : IPagesService
    {
        private readonly ApplicationDbContext dbContext;

        public PagesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ICollection<Page>> AddPagesAsync(IEnumerable<IFormFile> pages, string episodeDirectory, string episodeId)
        {
            var pagesToReturn = new List<Page>();
            var pageCounter = 0;

            foreach (var page in pages)
            {
                if (!this.IsImageValid(page))
                {
                    throw new ArgumentException("A invalid picture was found.Acceptable formats are png/jpg/jpeg with a maximum size of 1mb.");
                }

                pageCounter++;
                string pagePath = episodeDirectory + $"/page{pageCounter}.png";
                using (FileStream fs = new FileStream(pagePath, FileMode.Create))
                {
                    await page.CopyToAsync(fs);
                }

                var currentPage = new Page
                {
                    FilePath = pagePath,
                    EpisodeId = episodeId,
                };

                await this.dbContext.Pages.AddAsync(currentPage);

                pagesToReturn.Add(currentPage);
            }

            return pagesToReturn;
        }

        private bool IsImageValid(object value)
        {
            if (value is IFormFile file)
            {
                if (!(file.FileName.EndsWith(".png") || file.FileName.EndsWith(".jpg") || file.FileName.EndsWith(".jpeg")))
                {
                    return false;
                }

                if (file.Length > 1 * 1024 * 1024)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
