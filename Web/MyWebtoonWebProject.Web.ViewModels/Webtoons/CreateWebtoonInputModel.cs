namespace MyWebtoonWebProject.Web.ViewModels.Webtoons
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyWebtoonWebProject.Common.ValidationAttributes;
    using MyWebtoonWebProject.Data.Models.Enums;

    public class CreateWebtoonInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Title { get; set; }

        public string GenreId { get; set; }

        [Required]
        [MaxLength(600)]
        [DataType(DataType.MultilineText)]
        public string Synopsis { get; set; }

        public string AuthorId { get; set; }

        [Required]
        [IsValidImageAttribute]
        public IFormFile Cover { get; set; }

        public DayOfWeek UploadDay { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Genres { get; set; }
    }
}
