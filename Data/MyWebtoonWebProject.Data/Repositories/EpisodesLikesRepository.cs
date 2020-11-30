namespace MyWebtoonWebProject.Data.Repositories
{
    using MyWebtoonWebProject.Data.Models;

    public class EpisodesLikesRepository : EfRepository<EpisodeLike>, IEpisodesLikesRepository
    {
        public EpisodesLikesRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
