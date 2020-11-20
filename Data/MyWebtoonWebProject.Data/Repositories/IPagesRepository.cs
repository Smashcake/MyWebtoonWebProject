namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Collections.Generic;

    using MyWebtoonWebProject.Data.Common.Repositories;
    using MyWebtoonWebProject.Data.Models;

    public interface IPagesRepository : IRepository<Page>
    {
        ICollection<string> GetPagePathsForEpisodeByEpisodeId(string episodeId);
    }
}
