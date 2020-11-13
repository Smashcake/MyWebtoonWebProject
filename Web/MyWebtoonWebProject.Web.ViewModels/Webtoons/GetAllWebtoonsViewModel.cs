namespace MyWebtoonWebProject.Web.ViewModels.Webtoons
{
    using System.Collections.Generic;

    public class GetAllWebtoonsViewModel
    {
        public ICollection<GetWebtoonInfoViewModel> Webtoons { get; set; }
    }
}
