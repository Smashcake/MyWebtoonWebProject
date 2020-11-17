namespace MyWebtoonWebProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using MyWebtoonWebProject.Data.Models;

    public interface IPagesService
    {
        Task<ICollection<Page>> AddPagesAsync(IEnumerable<IFormFile> pages, string episodeDirectory, string episodeId);
    }
}
