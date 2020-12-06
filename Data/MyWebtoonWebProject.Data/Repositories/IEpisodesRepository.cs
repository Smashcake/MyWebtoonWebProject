namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Collections.Generic;

    using MyWebtoonWebProject.Data.Common.Repositories;
    using MyWebtoonWebProject.Data.Models;

    public interface IEpisodesRepository : IDeletableEntityRepository<Episode>
    {
        ICollection<Episode> GetEpisodesByWebtoonId(string webtoonId);

        Episode GetEpisodeByWebtoonTitleNumberAndEpisodeNumber(string webtoonTitleNumber, string episodeNumber);
    }
}
