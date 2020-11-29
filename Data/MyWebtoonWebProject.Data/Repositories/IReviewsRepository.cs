namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Collections.Generic;

    using MyWebtoonWebProject.Data.Common.Repositories;
    using MyWebtoonWebProject.Data.Models;

    public interface IReviewsRepository : IDeletableEntityRepository<Review>
    {
        ICollection<Review> GetReviewsByWebtoonId(string id);

        int GetReviewsCount();

        Review GetReviewByReviewNumber(string reviewNumber);
    }
}
