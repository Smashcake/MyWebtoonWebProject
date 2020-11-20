namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Linq;

    using MyWebtoonWebProject.Data.Models;

    public class GenresRepository : EfRepository<Genre>, IGenresRepository
    {
        public GenresRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public bool GenreExist(string name) =>
            this.DbSet
                .Any(g => g.Name == name);
    }
}
