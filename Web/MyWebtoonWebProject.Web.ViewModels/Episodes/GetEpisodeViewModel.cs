namespace MyWebtoonWebProject.Web.ViewModels.Episodes
{
    using System.Collections.Generic;

    public class GetEpisodeViewModel
    {
        public string WebtoonTitle { get; set; }

        public string EpisodeNumber { get; set; }

        public ICollection<string> PagesPaths { get; set; }
    }
}
