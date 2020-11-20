namespace MyWebtoonWebProject.Data.Repositories
{
    using MyWebtoonWebProject.Data.Common.Repositories;
    using MyWebtoonWebProject.Data.Models;

    public interface IGenresRepository : IRepository<Genre>
    {
        bool GenreExist(string name);
    }
}
