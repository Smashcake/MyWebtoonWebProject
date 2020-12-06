namespace MyWebtoonWebProject.Web.ViewModels.Episodes
{
    using System.Collections.Generic;

    using MyWebtoonWebProject.Data.Models;

    public class GetEpisodeViewModel
    {
        public string WebtoonTitle { get; set; }

        public string WebtoonTitleNumber { get; set; }

        public string EpisodeTitle { get; set; }

        public string EpisodeNumber { get; set; }

        public int Likes { get; set; }

        public string EpisodeAuthorId { get; set; }

        public ICollection<string> PagesPaths { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
