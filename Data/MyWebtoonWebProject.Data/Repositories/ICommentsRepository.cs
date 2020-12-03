namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Collections.Generic;

    using MyWebtoonWebProject.Data.Common.Repositories;
    using MyWebtoonWebProject.Data.Models;

    public interface ICommentsRepository : IDeletableEntityRepository<Comment>
    {
        public int Count();

        ICollection<Comment> GetEpisodeComments(string episodeId);

        public Comment GetCommentByCommentNumber(string commentNumber);
    }
}
