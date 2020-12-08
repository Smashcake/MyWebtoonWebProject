namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Linq;

    using MyWebtoonWebProject.Data.Models;

    public class ApplicationUserRepository : EfDeletableEntityRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public string GetWebtoonAuthorUsername(string webtoonAuthorId) =>
            this.DbSet
            .FirstOrDefault(ap => ap.Id == webtoonAuthorId).UserName;

        public string GetUsersUsernameById(string applicationUserId) =>
            this.DbSet
            .FirstOrDefault(ap => ap.Id == applicationUserId).UserName;
    }
}
