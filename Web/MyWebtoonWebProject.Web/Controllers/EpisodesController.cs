namespace MyWebtoonWebProject.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MyWebtoonWebProject.Web.ViewModels.Episodes;

    public class EpisodesController : Controller
    {
        public IActionResult AddEpisode(string id)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEpisode(AddEpisodeInputModel input)
        {

        }
    }
}
