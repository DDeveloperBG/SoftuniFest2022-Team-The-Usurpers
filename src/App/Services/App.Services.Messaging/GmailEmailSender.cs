namespace App.Services.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;

    using App.Web.ViewModels.EmailSender;

    public class GmailEmailSender : IEmailSender
    {
        private readonly GmailSenderCofigKeys cofigKeys;

        public GmailEmailSender(GmailSenderCofigKeys cofigKeys)
        {
            this.cofigKeys = cofigKeys;
        }

        public void SendEmail(string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(this.cofigKeys.Email);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = htmlContent;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(this.cofigKeys.Email, this.cofigKeys.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception)
            {
            }
        }
    }
}
