namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    public interface IWebtoonsRatingsService
    {
        Task RateWebtoonAsync(string webtoonTitleNumber, string userId, byte ratingValue);

        double GetWebtoonAverageRating(string webtoonTitleNumber);

        bool DoesWebtoonHaveARating(string webtoonTitleNumber);
    }
}
