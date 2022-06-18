namespace App.Web.Areas.BankEmployee.Controllers
{
    using App.Common;
    using App.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.BankEmployeeRoleName)]
    [Area("BankEmployee")]
    public class BankEmployeeController : BaseController
    {
        public IActionResult Dashboard()
        {
            return this.View();
        }
    }
}
