namespace MyWebtoonWebProject.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data;
    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public class WebtoonsService : IWebtoonsService
    {
        private readonly ApplicationDbContext dbContext;

        public WebtoonsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateWebtoonAsync(CreateWebtoonInputModel input)
        {
            if (this.dbContext.Webtoons.Any(w => w.Title == input.Title))
            {
                throw new ArgumentException("Webtoon already exists!");
            }

            string topFolder = @"C:\MyWebtoonWebProject\MyWebtoonWebProject\Web\MyWebtoonWebProject.Web\wwwroot\Webtoons";
            string webtoonFolder = Path.Combine(topFolder, input.Title);
            Directory.CreateDirectory(webtoonFolder);

            string coverPhotoPath = webtoonFolder + "/cover.png";
            using (FileStream fs = new FileStream(coverPhotoPath, FileMode.Create))
            {
                await input.Cover.CopyToAsync(fs);
            }

            var webtoon = new Webtoon
            {
                Title = input.Title,
                Synopsis = input.Synopsis,
                CoverPhoto = input.Title + "/cover.png",
                GenreId = input.GenreId,
                UploadDay = input.UploadDay,
                AuthorId = input.AuthorId,
                CreatedOn = DateTime.UtcNow,
                Rating = 0M,
                Completed = false,
                TitleNumber = (this.dbContext.Webtoons.Count() + 1).ToString(),
            };

            await this.dbContext.Webtoons.AddAsync(webtoon);
            await this.dbContext.SaveChangesAsync();
        }

        public ICollection<GetWebtoonInfoViewModel> GetAllWebtoons()
        {
            var webtoons = this.dbContext.Webtoons.Select(w => new GetWebtoonInfoViewModel
            {
                Author = w.Author.UserName,
                Title = w.Title,
                CoverPhoto = w.CoverPhoto,
                Genre = w.Genre.Name,
                Likes = w.Episodes.Sum(e => e.Likes),
                TitleNumber = w.TitleNumber,
            }).ToList();

            return webtoons;
        }

        public WebtoonInfoViewModel GetWebtoon(string titleNumber)
        {
            var webtoon = this.dbContext.Webtoons
                .Where(w => w.TitleNumber == titleNumber).Select(w => new WebtoonInfoViewModel
                {
                    Author = w.Author,
                    Episodes = w.Episodes,
                    Id = w.Id,
                    GenreName = w.Genre.Name,
                    Synopsis = w.Synopsis,
                    Title = w.Title,
                    CoverPhoto = w.CoverPhoto,
                    UploadDay = w.UploadDay.ToString(),
                    Reviews = w.Reviews,
                    TitleNumber = w.TitleNumber,
                }).FirstOrDefault();

            return webtoon;
        }
    }
}
