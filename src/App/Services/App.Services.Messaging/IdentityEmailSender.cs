namespace App.Services.Messaging
{
    using System.Threading.Tasks;

    public class IdentityEmailSender : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
    {
        private readonly IEmailSender emailSender;

        public IdentityEmailSender(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            this.emailSender.SendEmail(email, subject, htmlMessage);
            return Task.CompletedTask;
        }
    }
}
