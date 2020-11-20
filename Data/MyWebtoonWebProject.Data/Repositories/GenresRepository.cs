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

        public Genre GetGenreByWebtoonGenreId(string webtoonGenreId) =>
            this.DbSet
                .FirstOrDefault(g => g.Id == webtoonGenreId);
    }
}
