namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Linq;

    using MyWebtoonWebProject.Data.Models;

    public class CommentsRepository : EfDeletableEntityRepository<Comment>, ICommentsRepository
    {
        public CommentsRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public int Count()
            => this.DbSet.Count();
    }
}
