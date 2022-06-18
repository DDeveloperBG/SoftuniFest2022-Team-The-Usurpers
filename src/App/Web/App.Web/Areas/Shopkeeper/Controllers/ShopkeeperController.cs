namespace App.Web.Areas.Shopkeeper.Controllers
{
    using System.Threading.Tasks;

    using App.Common;
    using App.Data.Models;
    using App.Services.Data.UpdateRecords;
    using App.Web.Controllers;
    using App.Web.ViewModels.Shopkeeper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.ShopkeeperRoleName)]
    [Area("Shopkeeper")]
    public class ShopkeeperController : BaseController
    {
        private readonly IShopkeepersService shopkeepersService;
        private readonly UserManager<ApplicationUser> userManager;

        public ShopkeeperController(
            IShopkeepersService shopkeepersService,
            UserManager<ApplicationUser> userManager)
        {
            this.shopkeepersService = shopkeepersService;
            this.userManager = userManager;
        }

        public IActionResult Dashboard()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult AddDiscount()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscountAsync(DiscountInputModel input)
        {
            if ((input.StartDate - input.EndDate).TotalSeconds >= 0)
            {
                this.ModelState.AddModelError(string.Empty, "Field Valid from can't be less or equal to Valid until");
                return this.View(input);
            }

            string userId = this.userManager.GetUserId(this.User);
            await this.shopkeepersService.AddDiscountAsync(input, userId);

            return this.RedirectToAction(nameof(this.Dashboard));
        }

        [HttpGet]
        public IActionResult AllDiscounts()
        {
            var discounts = this.shopkeepersService.GetAllDiscoundsMapped<DiscountViewModel>();

            return this.View(discounts);
        }
    }
}
