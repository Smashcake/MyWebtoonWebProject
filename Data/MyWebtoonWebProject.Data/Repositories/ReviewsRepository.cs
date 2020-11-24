namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using MyWebtoonWebProject.Data.Models;

    public class ReviewsRepository : EfDeletableEntityRepository<Review>, IReviewsRepository
    {
        public ReviewsRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public ICollection<Review> GetReviewsByWebtoonId(string webtoonId)
        {
            return this.DbSet.Where(r => r.WebtoonId == webtoonId).ToList();
        }
    }
}
