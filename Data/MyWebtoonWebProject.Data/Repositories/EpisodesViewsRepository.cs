namespace MyWebtoonWebProject.Data.Repositories
{
    using MyWebtoonWebProject.Data.Models;

    public class EpisodesViewsRepository : EfRepository<EpisodeView>, IEpisodesViewsRepository
    {
        public EpisodesViewsRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
