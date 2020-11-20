namespace MyWebtoonWebProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data;
    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Repositories;
    using MyWebtoonWebProject.Web.ViewModels.Genres;

    public class GenresService : IGenresService
    {
        private readonly IGenresRepository genresRepository;

        public GenresService(IGenresRepository genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public async Task CreateGenreAsync(CreateGenreInputModel input)
        {
            if (this.genresRepository.GenreExist(input.Name))
            {
                throw new ArgumentException("Genre already exists!");
            }

            var genreToAdd = new Genre
            {
                Name = input.Name,
            };
            await this.genresRepository.AddAsync(genreToAdd);
            await this.genresRepository.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.genresRepository.AllAsNoTracking().Select(g => new
            {
                g.Id,
                g.Name,
            }).ToList().Select(g => new KeyValuePair<string, string>(g.Id, g.Name));
        }
    }
}
