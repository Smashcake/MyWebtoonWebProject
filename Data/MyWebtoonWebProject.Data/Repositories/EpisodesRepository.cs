namespace MyWebtoonWebProject.Data.Repositories
{
    using MyWebtoonWebProject.Data.Models;

    public class EpisodesRepository : EfDeletableEntityRepository<Episode>, IEpisodesRepository
    {
        public EpisodesRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
