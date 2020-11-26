namespace MyWebtoonWebProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyWebtoonWebProject.Data.Repositories;
    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public class ApplicationUsersService : IAppicationUsersService
    {
        private readonly IWebtoonsRepository webtoonsRepository;

        public ApplicationUsersService(IWebtoonsRepository webtoonsRepository)
        {
            this.webtoonsRepository = webtoonsRepository;
        }

        public ICollection<GetWebtoonInfoViewModel> GetUserSubscribtions(string userId)
        {
            var webtoonsInfo = this.webtoonsRepository
                .AllAsNoTracking()
                .Where(w => w.Subscribers.Any(s => s.SubscriberId == userId))
                .Select(w => new GetWebtoonInfoViewModel
                {
                    Author = w.Author.UserName,
                    CoverPhoto = w.CoverPhoto,
                    Genre = w.Genre.Name,
                    Likes = w.Episodes.Sum(e => e.Likes),
                    Title = w.Title,
                    TitleNumber = w.TitleNumber,
                }).ToList();

            return webtoonsInfo;
        }
    }
}
