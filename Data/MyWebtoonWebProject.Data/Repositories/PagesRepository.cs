namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using MyWebtoonWebProject.Data.Models;

    public class PagesRepository : EfRepository<Page>, IPagesRepository
    {
        public PagesRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public ICollection<string> GetPagePathsForEpisodeByEpisodeId(string episodeId) =>
            this.DbSet
                .Where(p => p.EpisodeId == episodeId)
                .OrderBy(p => p.PageNumber)
                .Select(p => p.FilePath)
                .ToList();
    }
}
