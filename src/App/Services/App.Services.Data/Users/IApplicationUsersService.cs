namespace App.Services.Data.Users
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IApplicationUsersService
    {
        bool GetNotificationsState(ClaimsPrincipal userClaims);

        Task ChangeNotificationsStateAsync(ClaimsPrincipal userClaims);
    }
}
