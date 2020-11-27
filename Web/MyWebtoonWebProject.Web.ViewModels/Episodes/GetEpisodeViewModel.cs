namespace MyWebtoonWebProject.Web.ViewModels.Episodes
{
    using System.Collections.Generic;

    using MyWebtoonWebProject.Data.Models;

    public class GetEpisodeViewModel
    {
        public string WebtoonTitle { get; set; }

        public string EpisodeNumber { get; set; }

        public ICollection<string> PagesPaths { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
