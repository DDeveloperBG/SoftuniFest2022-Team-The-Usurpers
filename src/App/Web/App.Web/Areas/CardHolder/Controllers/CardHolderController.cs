namespace App.Web.Controllers
{
    using App.Common;
    using App.Services.Data.UpdateRecords;
    using App.Web.ViewModels.CardHolder;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.CardHolderRoleName)]
    [Area("CardHolder")]
    public class CardHolderController : BaseController
    {
        private readonly ICardHoldersService cardHoldersService;

        public CardHolderController(ICardHoldersService cardHoldersService)
        {
            this.cardHoldersService = cardHoldersService;
        }

        public IActionResult Index()
        {
            var activeDiscounts = this.cardHoldersService
                .GetAllActiveDiscounts<ActiveDicountViewModel>();

            return this.View(activeDiscounts);
        }
    }
}
