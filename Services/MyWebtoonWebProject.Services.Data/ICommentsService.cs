namespace MyWebtoonWebProject.Services.Data
{
    using System.Threading.Tasks;

    using MyWebtoonWebProject.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task CreateComment(CommentInputModel input, string userId);

        Task DeleteComment(string commentNumber, string userId);
    }
}
