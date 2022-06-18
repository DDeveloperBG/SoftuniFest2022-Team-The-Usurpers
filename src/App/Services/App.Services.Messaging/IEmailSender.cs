namespace App.Services.Messaging
{
    using System.Collections.Generic;

    public interface IEmailSender
    {
        void SendEmail(
            string to,
            string subject,
            string htmlContent,
            IEnumerable<EmailAttachment> attachments = null);
    }
}
