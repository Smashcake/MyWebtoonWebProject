namespace MyWebtoonWebProject.Web.ViewModels.Episodes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyWebtoonWebProject.Common.ValidationAttributes;

    public class AddEpisodeInputModel
    {
        public string WebtoonId { get; set; }

        [Required]
        [IsValidImageAttribute]
        public IEnumerable<IFormFile> Pages { get; set; }
    }
}
