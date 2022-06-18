namespace App.Web.Controllers
{
    using System.Threading.Tasks;

    using App.Services.Data.Users;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class UserController : BaseController
    {
        private readonly IApplicationUsersService applicationUsersService;

        public UserController(IApplicationUsersService applicationUsersService)
        {
            this.applicationUsersService = applicationUsersService;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ChangeNotificationsStateAsync()
        {
            await this.applicationUsersService.ChangeNotificationsStateAsync(this.User);

            return this.Ok();
        }
    }
}
