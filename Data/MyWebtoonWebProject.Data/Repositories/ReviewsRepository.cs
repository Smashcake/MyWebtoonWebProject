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

        public Review GetReviewByReviewNumber(string reviewNumber)
        {
            return this.DbSet.FirstOrDefault(r => r.ReviewNumber == reviewNumber);
        }

        public ICollection<Review> GetReviewsByWebtoonId(string webtoonId)
        {
            return this.DbSet.Where(r => r.WebtoonId == webtoonId).ToList();
        }

        public int GetReviewsCount() => this.DbSet.Count();
    }
}
