namespace App.Services.Messaging
{
    public interface IEmailSender
    {
        void SendEmail(
            string to,
            string subject,
            string htmlContent);
    }
}
