namespace MyWebtoonWebProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyWebtoonWebProject.Data.Common.Models;

    public class Page : BaseModel<string>
    {
        public Page()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string EpisodeId { get; set; }

        public Episode Episode { get; set; }

        [Required]
        public string FilePath { get; set; }

        public short PageNumber { get; set; }

        [Required]
        public string FileExtention { get; set; }
    }
}
