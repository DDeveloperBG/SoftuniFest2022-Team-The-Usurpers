namespace App.Services.Data.Notifications
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface INotificationsService
    {
        bool GetNotificationsState(ClaimsPrincipal userClaims);

        Task ChangeNotificationsStateAsync(ClaimsPrincipal userClaims);

        void SendNotification(string userId, string subject, string htmlContent);

        Task NotifyCardHoldersAboutNewAcceptedDiscountsAsync();
    }
}
