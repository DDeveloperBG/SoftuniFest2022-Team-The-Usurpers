namespace App.Web.Infrastructure.Middlewares
{
    using System.Threading.Tasks;

    using App.Common;
    using App.Data.Models;
    using App.Services.Data.UpdateRecords;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class UseCheckShopkeeperPasswordMiddleware
    {
        private readonly RequestDelegate next;

        public UseCheckShopkeeperPasswordMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            UserManager<ApplicationUser> userManager,
            IShopkeepersService shopkeepersService)
        {
            if (context.Request.Method == "GET")
            {
                if (context.User.IsInRole(GlobalConstants.ShopkeeperRoleName))
                {
                    var user = await userManager.GetUserAsync(context.User);

                    var changePassLink = "/Identity/Account/Manage/ChangePassword/";
                    if (shopkeepersService.HasToChangePassword(user) && context.Request.Path != changePassLink)
                    {
                        context.Response.Redirect(changePassLink);
                        return;
                    }
                }
            }

            await this.next(context);
        }
    }
}
