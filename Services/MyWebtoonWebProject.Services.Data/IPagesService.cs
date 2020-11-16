namespace MyWebtoonWebProject.Services.Data
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public interface IPagesService
    {
        void AddPages(IEnumerable<IFormFile> pages);
    }
}
