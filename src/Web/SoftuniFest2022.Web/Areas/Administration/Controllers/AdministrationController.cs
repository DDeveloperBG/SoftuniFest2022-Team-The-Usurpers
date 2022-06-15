namespace SoftuniFest2022.Web.Areas.Administration.Controllers
{
    using SoftuniFest2022.Common;
    using SoftuniFest2022.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
