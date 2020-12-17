namespace MyWebtoonWebProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
            return this.genresRepository.All()
                .Select(g => new
                {
                    g.Id,
                    g.Name,
                }).ToList()
            .Select(g => new KeyValuePair<string, string>(g.Id, g.Name));
        }

        public ICollection<GenreViewModel> AllGenres()
        {
            var genres = this.genresRepository.All()
                .Select(g => new GenreViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                }).ToList();

            return genres;
        }

        public async Task DeleteGenre(string id)
        {
            var genre = this.genresRepository.All().FirstOrDefault(g => g.Id == id);

            if (genre == null)
            {
                throw new ArgumentNullException("WTF BRO");
            }

            this.genresRepository.Delete(genre);
            await this.genresRepository.SaveChangesAsync();
        }

        public string GetGenreName(string id) =>
            this.genresRepository.All()
            .FirstOrDefault(g => g.Id == id).Name;

        public async Task EditGenre(EditGenreInputModel input)
        {
            var genre = this.genresRepository.All().FirstOrDefault(g => g.Id == input.Id);

            if (genre == null)
            {
                throw new ArgumentNullException("WTF BRO");
            }

            genre.Name = input.Name;
            await this.genresRepository.SaveChangesAsync();
        }
    }
}
