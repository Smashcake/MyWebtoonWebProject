namespace MyWebtoonWebProject.Data.Repositories
{
    using MyWebtoonWebProject.Data.Common.Repositories;
    using MyWebtoonWebProject.Data.Models;

    public interface IApplicationUserRepository : IDeletableEntityRepository<ApplicationUser>
    {
        public string GetWebtoonAuthorUsername(string webtoonAuthorId);

        public string GetUsersUsernameById(string applicationUserId);
    }
}
