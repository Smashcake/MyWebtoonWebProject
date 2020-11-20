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

        public ICollection<Episode> GetEpisodesByWebtoonId(string webtoonId) =>
            this.DbSet
            .Where(e => e.WebtoonId == webtoonId).ToList();
    }
}
