namespace App.Web.Controllers
{
    using System.Threading.Tasks;

    using App.Services.Data.Notifications;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class UserController : BaseController
    {
        private readonly INotificationsService notificationsService;

        public UserController(INotificationsService notificationsService)
        {
            this.notificationsService = notificationsService;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ChangeNotificationsStateAsync()
        {
            await this.notificationsService.ChangeNotificationsStateAsync(this.User);

            return this.Ok();
        }
    }
}
