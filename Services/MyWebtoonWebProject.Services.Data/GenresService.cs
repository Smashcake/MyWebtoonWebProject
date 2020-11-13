namespace MyWebtoonWebProject.Services
{
    using System;
    using System.Collections.Generic;
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

        public async Task CreateGenreAsync(CreateGenreInputModel input)
        {
            if (this.dbContext.Genres.Any(g => g.Name == input.Name))
            {
                throw new ArgumentException("Genre already exists!");
            }

            var genreToAdd = new Genre
            {
                Name = input.Name,
            };
            await this.dbContext.Genres.AddAsync(genreToAdd);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.dbContext.Genres.Select(g => new
            {
                g.Id,
                g.Name,
            }).ToList().Select(g => new KeyValuePair<string, string>(g.Id, g.Name));
        }
    }
}
