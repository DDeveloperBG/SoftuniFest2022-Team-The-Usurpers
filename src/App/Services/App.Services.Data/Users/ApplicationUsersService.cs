namespace App.Services.Data.Users
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using App.Data.Common.Repositories;
    using App.Data.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUsersService : IApplicationUsersService
    {
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly IRepository<ApplicationUser> users;

        public ApplicationUsersService(
            UserManager<ApplicationUser> usermanager,
            IRepository<ApplicationUser> users)
        {
            this.usermanager = usermanager;
            this.users = users;
        }

        public Task ChangeNotificationsStateAsync(ClaimsPrincipal userClaims)
        {
            var userId = this.usermanager.GetUserId(userClaims);
            var user = this.users.All().Where(x => x.Id == userId).Single();

            user.ReceiveNotifications = !user.ReceiveNotifications;

            return this.users.SaveChangesAsync();
        }

        public bool GetNotificationsState(ClaimsPrincipal userClaims)
        {
            var userId = this.usermanager.GetUserId(userClaims);
            var user = this.users.All().Where(x => x.Id == userId).Single();

            return user.ReceiveNotifications;
        }
    }
}
