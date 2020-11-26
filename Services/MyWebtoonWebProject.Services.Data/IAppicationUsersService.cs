namespace MyWebtoonWebProject.Services.Data
{
    using System.Collections.Generic;

    using MyWebtoonWebProject.Web.ViewModels.Webtoons;

    public interface IAppicationUsersService
    {
        ICollection<GetWebtoonInfoViewModel> GetUserSubscribtions(string userId);
    }
}
