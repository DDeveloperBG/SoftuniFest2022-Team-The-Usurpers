namespace App.Web.Controllers
{
    using App.Common;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.CardHolderRoleName)]
    [Area("CardHolder")]
    public class CardHolderController : BaseController
    {
        public IActionResult Dashboard()
        {
            return this.View();
        }
    }
}
