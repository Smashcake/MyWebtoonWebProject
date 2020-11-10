namespace MyWebtoonWebProject.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data;
    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Web.ViewModels.Genres;

    public class GenresService : IGenresService
    {
        private readonly ApplicationDbContext dbContext;

        public GenresService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreateGenre(CreateGenreInputModel input)
        {
            if (this.dbContext.Genres.Any(g => g.Name == input.Name))
            {
                return -1;
            }

            var genreToAdd = new Genre
            {
                Name = input.Name,
            };
            this.dbContext.Genres.Add(genreToAdd);
            return await this.dbContext.SaveChangesAsync();
        }
    }
}
