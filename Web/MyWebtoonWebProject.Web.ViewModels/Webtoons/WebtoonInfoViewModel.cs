namespace MyWebtoonWebProject.Web.ViewModels.Webtoons
{
    using System;
    using System.Collections.Generic;

    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Web.ViewModels.Episodes;

    public class WebtoonInfoViewModel
    {
        public string CoverPhoto { get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string GenreName { get; set; }

        public string UploadDay { get; set; }

        public string Synopsis { get; set; }

        public string TitleNumber { get; set; }

        public IEnumerable<EpisodeWebtoonViewModel> Episodes { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public int PageNumber { get; set; }

        public bool HasPreviousEpisodePage => this.PageNumber > 1;

        public int PreviousEpisodePage => this.PageNumber - 1;

        public bool HasNextEpisodePage => this.PageNumber < this.EpisodePagesCount;

        public int NextEpisodePage => this.PageNumber + 1;

        public int EpisodePagesCount => (int)Math.Ceiling((double)this.EpisodesCount / this.EpisodesPerPage);

        public int EpisodesPerPage { get; set; }

        public int EpisodesCount { get; set; }
    }
}
