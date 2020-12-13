namespace MyWebtoonWebProject.Data.Repositories
{

    using MyWebtoonWebProject.Data.Models;

    public class WebtoonsRatingsRepository : EfRepository<WebtoonRating>, IWebtoonsRatingsRepository
    {
        public WebtoonsRatingsRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
