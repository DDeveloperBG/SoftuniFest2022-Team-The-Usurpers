namespace App.Services.Data.Notifications
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using App.Data.Common.Repositories;
    using App.Data.Models;
    using App.Services.Mapping;
    using App.Services.Messaging;
    using App.Web.ViewModels.CardHolder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Razor.Templating.Core;

    public class NotificationsService : INotificationsService
    {
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly IRepository<ApplicationUser> users;
        private readonly IRepository<Discount> discounts;
        private readonly IEmailSender emailSender;
        private readonly IWebHostEnvironment hostingEnvironment;

        public NotificationsService(
            UserManager<ApplicationUser> usermanager,
            IRepository<ApplicationUser> users,
            IEmailSender emailSender,
            IRepository<Discount> discounts,
            IWebHostEnvironment hostingEnvironment)
        {
            this.usermanager = usermanager;
            this.users = users;
            this.emailSender = emailSender;
            this.discounts = discounts;
            this.hostingEnvironment = hostingEnvironment;
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

        public async Task NotifyCardHoldersAboutNewAcceptedDiscountsAsync()
        {
            var utcNow = DateTime.UtcNow;
            var lastTimeOfNotifying = utcNow.AddDays(-7);
            var newDiscounts = this.discounts
                 .AllAsNoTracking()
                 .Where(x => x.Status == DiscountStatus.Active)
                 .Where(x => x.EndDate > utcNow)
                 .Where(x => x.CreatedOn >= lastTimeOfNotifying)
                 .To<ActiveDicountViewModel>()
                 .ToList();

            if (newDiscounts.Count == 0)
            {
                return;
            }

            string subject = "New Discounts";

            string htmlPage = await RazorTemplateEngine.RenderAsync("~/Areas/CardHolder/Views/CardHolder/Index.cshtml", newDiscounts);
            int fromIndex = htmlPage.IndexOf("<!-- START -->");
            int toIndex = htmlPage.IndexOf("<!-- END -->");
            string htmlContent = htmlPage[fromIndex..toIndex];

            var usersEmails = this.users
                .AllAsNoTracking()
                .Where(x => x.ReceiveNotifications)
                .Select(x => x.Email)
                .ToList();

            foreach (var userEmail in usersEmails)
            {
                this.emailSender.SendEmail(userEmail, subject, htmlContent.ToString());
            }
        }

        public void SendNotification(string userId, string subject, string htmlContent)
        {
            var userData = this.users
                .AllAsNoTracking()
                .Where(x => x.Id == userId)
                .Select(x => new { x.Email, x.ReceiveNotifications })
                .Single();

            if (!userData.ReceiveNotifications)
            {
                return;
            }

            this.emailSender.SendEmail(userData.Email, subject, htmlContent);
        }
    }
}
