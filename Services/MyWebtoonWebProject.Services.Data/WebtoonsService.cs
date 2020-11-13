namespace MyWebtoonWebProject.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Data;
    using MyWebtoonWebProject.Data.Common.Repositories;
    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public class WebtoonsService : IWebtoonsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDeletableEntityRepository<Webtoon> webtoonsRepository;

        public WebtoonsService(ApplicationDbContext dbContext, IDeletableEntityRepository<Webtoon> webtoonsRepository)
        {
            this.dbContext = dbContext;
            this.webtoonsRepository = webtoonsRepository;
        }

        public async Task<int> CreateWebtoonAsync(CreateWebtoonInputModel input)
        {
            string topFolder = @"c:\MyWebtoonWebProject\Webtoons";
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
                CoverPhoto = coverPhotoPath,
                GenreId = input.GenreId,
                UploadDay = input.UploadDay,
                AuthorId = input.AuthorId,
                CreatedOn = DateTime.UtcNow,
                Rating = 0M,
                Completed = false,
            };

            await this.webtoonsRepository.AddAsync(webtoon);
            await this.webtoonsRepository.SaveChangesAsync();

            return 0;
        }
    }
}
