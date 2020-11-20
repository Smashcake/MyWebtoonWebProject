namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using MyWebtoonWebProject.Data.Models;

    public class WebtoonsRepository : EfDeletableEntityRepository<Webtoon>, IWebtoonsRepository
    {
        public WebtoonsRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public bool WebtoonExists(string title) =>
            this.DbSet
                .Any(w => w.Title == title);

        public int GetWebtoonsCount() =>
            this.DbSet
                .Count();

        public Webtoon GetWebtoonByTitleNumber(string titleNumber) =>
            this.DbSet
                .FirstOrDefault(w => w.TitleNumber == titleNumber);
    }
}
