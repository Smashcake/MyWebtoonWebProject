namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using MyWebtoonWebProject.Data.Models;

    public class EpisodesRepository : EfDeletableEntityRepository<Episode>, IEpisodesRepository
    {
        public EpisodesRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public Episode GetEpisodeByWebtoonTitleNumberAndEpisodeNumber(string webToonId, string episodeNumber) =>
            this.DbSet
            .FirstOrDefault(e => e.Webtoon.Id == webToonId && e.EpisodeNumber == episodeNumber);

        public ICollection<Episode> GetEpisodesByWebtoonId(string webtoonId) =>
            this.DbSet
            .Where(e => e.WebtoonId == webtoonId).ToList();
    }
}
