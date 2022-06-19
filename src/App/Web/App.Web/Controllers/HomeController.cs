namespace App.Web.Controllers
{
    using System.Diagnostics;

    using App.Common;
    using App.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (this.User.IsInRole(GlobalConstants.ShopkeeperRoleName))
            {
                return this.LocalRedirect("/Shopkeeper/Shopkeeper/Dashboard");
            }
            else if (this.User.IsInRole(GlobalConstants.BankEmployeeRoleName))
            {
                return this.LocalRedirect("/BankEmployee/BankEmployee/Dashboard");
            }
            else if (this.User.IsInRole(GlobalConstants.CardHolderRoleName))
            {
                return this.LocalRedirect("/CardHolder/CardHolder");
            }
            else if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.LocalRedirect("/Administration/Dashboard");
            }

            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
