namespace MyWebtoonWebProject.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using MyWebtoonWebProject.Data.Models;

    public class CommentsRepository : EfDeletableEntityRepository<Comment>, ICommentsRepository
    {
        public CommentsRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public int Count()
            => this.DbSet.Count();

        public ICollection<Comment> GetEpisodeComments(string episodeId)
        {
            return this.DbSet.Where(c => c.EpisodeId == episodeId).ToList();
        }

        public Comment GetCommentByCommentNumber(string commentNumber)
        {
            return this.DbSet.FirstOrDefault(c => c.CommentNumber == commentNumber);
        }
    }
}
