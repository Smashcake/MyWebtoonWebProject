namespace MyWebtoonWebProject.Data.Repositories
{
    using MyWebtoonWebProject.Data.Models;

    public class PagesRepository : EfRepository<Page>, IPagesRepository
    {
        public PagesRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
