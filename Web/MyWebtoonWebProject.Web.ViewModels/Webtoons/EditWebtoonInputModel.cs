namespace MyWebtoonWebProject.Web.ViewModels.Webtoons
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MyWebtoonWebProject.Common.ValidationAttributes;
    using MyWebtoonWebProject.Data.Models.Enums;

    public class EditWebtoonInputModel
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

        public DayOfWeek UploadDay { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Genres { get; set; }

        [Required]
        public string OldTitle { get; set; }

        [Required]
        public string WebtoonTitleNumber { get; set; }
    }
}
