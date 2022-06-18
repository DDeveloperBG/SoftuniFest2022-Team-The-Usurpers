namespace App.Web.Areas.BankEmployee.Controllers
{
    using System.Threading.Tasks;

    using App.Common;
    using App.Data.Models;
    using App.Services.Data.UpdateRecords;
    using App.Web.Controllers;
    using App.Web.ViewModels.BankEmployee;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.BankEmployeeRoleName)]
    [Area("BankEmployee")]
    public class BankEmployeeController : BaseController
    {
        private readonly IBankEmployeesService bankEmployeesService;
        private readonly UserManager<ApplicationUser> userManager;

        public BankEmployeeController(
            UserManager<ApplicationUser> userManager,
            IBankEmployeesService bankEmployeesService)
        {
            this.userManager = userManager;
            this.bankEmployeesService = bankEmployeesService;
        }

        public IActionResult Dashboard()
        {
            return this.View();
        }

        public IActionResult AllDiscounts()
        {
            var userId = this.userManager.GetUserId(this.User);
            var discounts = this.bankEmployeesService.GetAllDiscounts(userId);

            return this.View(discounts);
        }

        public async Task<IActionResult> ChangeDiscountStatusAsync(string discountId, sbyte vote)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.bankEmployeesService.ChangeDiscountStatusAsync(discountId, userId, vote);

            return this.RedirectToAction(nameof(this.AllDiscounts));
        }

        public IActionResult AllShopkeepers()
        {
            var shopkeepers = this.bankEmployeesService.GetAllShopkeepers<ShopkeeperViewModel>();

            return this.View(shopkeepers);
        }

        public IActionResult AllTerminals()
        {
            var shopkeepers = this.bankEmployeesService.GetAllTerminals<TerminalViewModel>();

            return this.View(shopkeepers);
        }
    }
}
