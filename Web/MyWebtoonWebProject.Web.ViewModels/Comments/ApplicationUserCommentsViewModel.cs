namespace MyWebtoonWebProject.Web.ViewModels.Comments
{
    using System.Collections.Generic;

    public class ApplicationUserCommentsViewModel
    {
        public ICollection<ApplicationUserCommentViewModel> Comments { get; set; }
    }
}
