namespace MyWebtoonWebProject.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data;
    using MyWebtoonWebProject.Data.Common.Repositories;
    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Web.ViewModels.Genres;

    public class GenresService : IGenresService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IRepository<Genre> genresRepository;

        public GenresService(ApplicationDbContext dbContext, IRepository<Genre> genresRepository)
        {
            this.dbContext = dbContext;
            this.genresRepository = genresRepository;
        }

        public async Task<int> CreateGenreAsync(CreateGenreInputModel input)
        {
            if (this.dbContext.Genres.Any(g => g.Name == input.Name))
            {
                return -1;
            }

            var genreToAdd = new Genre
            {
                Name = input.Name,
            };
            await this.genresRepository.AddAsync(genreToAdd);
            await this.genresRepository.SaveChangesAsync();

            return 0;
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
