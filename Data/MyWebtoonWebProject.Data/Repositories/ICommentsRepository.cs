namespace MyWebtoonWebProject.Data.Repositories
{
    using MyWebtoonWebProject.Data.Common.Repositories;
    using MyWebtoonWebProject.Data.Models;

    public interface ICommentsRepository : IDeletableEntityRepository<Comment>
    {
        public int Count();
    }
}
