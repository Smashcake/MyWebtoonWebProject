namespace MyWebtoonWebProject.Services
{
    using System.Linq;

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

        public void CreateGenre(CreateGenreInputModel input)
        {
            if (this.dbContext.Genres.Any(g => g.Name == input.Name))
            {
                return;
            }

            var genreToAdd = new Genre
            {
                Name = input.Name,
            };
            this.dbContext.Genres.Add(genreToAdd);
            this.dbContext.SaveChangesAsync();
        }
    }
}
