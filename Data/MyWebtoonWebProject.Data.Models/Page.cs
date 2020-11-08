namespace MyWebtoonWebProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Page
    {
        public Page()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string EpisodeId { get; set; }

        public Episode Episode { get; set; }

        [Required]
        public string FilePath { get; set; }
    }
}
