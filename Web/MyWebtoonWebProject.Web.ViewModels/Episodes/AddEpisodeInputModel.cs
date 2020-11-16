namespace MyWebtoonWebProject.Web.ViewModels.Episodes
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class AddEpisodeInputModel
    {
        public string WebtoonId { get; set; }

        public IEnumerable<IFormFile> Pages { get; set; }
    }
}
