namespace MyWebtoonWebProject.Data.Repositories
{
    using MyWebtoonWebProject.Data.Common.Repositories;
    using MyWebtoonWebProject.Data.Models;

    public interface IWebtoonsRepository : IDeletableEntityRepository<Webtoon>
    {
        bool WebtoonExists(string title);

        int GetWebtoonsCount();

        Webtoon GetWebtoonByTitleNumber(string titleNumber);
    }
}