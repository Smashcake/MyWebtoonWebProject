namespace MyWebtoonWebProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyWebtoonWebProject.Data;
    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Repositories;

    public class PagesService : IPagesService
    {
        private readonly IPagesRepository pagesRepository;

        public PagesService(IPagesRepository pagesRepository)
        {
            this.pagesRepository = pagesRepository;
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
                string pageName = $"/page{pageCounter}.png";
                string pagePath = episodeDirectory + pageName;
                using (FileStream fs = new FileStream(pagePath, FileMode.Create))
                {
                    await page.CopyToAsync(fs);
                }

                var currentPage = new Page
                {
                    FilePath = pageName,
                    EpisodeId = episodeId,
                };

                await this.pagesRepository.AddAsync(currentPage);

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
