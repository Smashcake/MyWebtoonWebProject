namespace MyWebtoonWebProject.Services.Data
{
    using System.Collections.Generic;
    using System.IO;

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

        public async void AddPages(IEnumerable<IFormFile> pages, string episodeDirectory, string episodeId)
        {
            var pagesToReturn = new List<Page>();
            var pageCounter = 0;

            foreach (var page in pages)
            {
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
    }
}
