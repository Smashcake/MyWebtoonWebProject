namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task CreateCommentAsync(CommentInputModel input, string userId);

        Task DeleteCommentAsync(string commentNumber, string userId);

        Task EditCommentAsync(string commentNumber, string userId, string commentInfo);
    }
}
