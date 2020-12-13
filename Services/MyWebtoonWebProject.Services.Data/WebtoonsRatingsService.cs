namespace MyWebtoonWebProject.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using MyWebtoonWebProject.Data.Models;
    using MyWebtoonWebProject.Data.Repositories;

    public class WebtoonsRatingsService : IWebtoonsRatingsService
    {
        private readonly IWebtoonsRatingsRepository webtoonsRatingsRepository;
        private readonly IWebtoonsRepository webtoonsRepository;

        public WebtoonsRatingsService(IWebtoonsRatingsRepository webtoonsRatingsRepository, IWebtoonsRepository webtoonsRepository)
        {
            this.webtoonsRatingsRepository = webtoonsRatingsRepository;
            this.webtoonsRepository = webtoonsRepository;
        }

        public async Task RateWebtoonAsync(string webtoonTitleNumber, string userId, byte ratingValue)
        {
            var webtoonId = this.webtoonsRepository.GetWebtoonByTitleNumber(webtoonTitleNumber).Id;

            var userWebtoonRating = this.webtoonsRatingsRepository.All()
                .FirstOrDefault(wr => wr.ApplicationUserId == userId && wr.WebtoonId == webtoonId);

            if (userWebtoonRating == null)
            {
                userWebtoonRating = new WebtoonRating
                {
                    WebtoonId = webtoonId,
                    ApplicationUserId = userId,
                };

                await this.webtoonsRatingsRepository.AddAsync(userWebtoonRating);
            }

            userWebtoonRating.RatingValue = ratingValue;
            await this.webtoonsRatingsRepository.SaveChangesAsync();
        }

        public double GetWebtoonAverageRating(string webtoonTitleNumber)
        {
            var webtoonId = this.webtoonsRepository.GetWebtoonByTitleNumber(webtoonTitleNumber).Id;
            var averageWebtoonRating = this.webtoonsRatingsRepository.All()
                .Where(wr => wr.WebtoonId == webtoonId)
                .Average(wr => wr.RatingValue);

            return averageWebtoonRating;
        }
    }
}
