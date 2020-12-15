namespace MyWebtoonWebProject.Web.ViewModels.Episodes
{
    using System;

    public class LatestEpisodeViewModel
    {
        public string WebtoonCoverPhoto { get; set; }

        public string WebtoonTitle { get; set; }

        public string WebtoonTitleNumber { get; set; }

        public string WebtoonGenreName { get; set; }

        public string EpisodeTitle { get; set; }

        public string EpisodeNumber { get; set; }

        public DateTime EpisodeCreatedOn { get; set; }
    }
}
