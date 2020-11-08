namespace MyWebtoonWebProject.Web.Areas.Administration.Controllers
{
    using MyWebtoonWebProject.Common;
    using MyWebtoonWebProject.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
