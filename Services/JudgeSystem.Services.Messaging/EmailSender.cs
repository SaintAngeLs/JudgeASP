using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Content = Amazon.SimpleEmail.Model.Content;
// smtp user name: AKIAUOKGVHWGZTAAQHPU
// smtp password: BA1Go3ctNNcFuYd6qCELbcDvmzHt/+Zl0X8GLdt5Ex1D
// IAM user name: ses-smtp-user.20230728-030324.justice

// SMTP endpoint: email-smtp.us-east-1.amazonaws.com
namespace JudgeSystem.Services.Messaging
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<BaseEmailOptions> emailOptions)
        {
            EmailOptions = emailOptions.Value;
        }

        public BaseEmailOptions EmailOptions { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(subject, message, email);
        }

        public async Task Execute(string subject, string message, string email)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();

                mailMessage.From = new MailAddress(EmailOptions.Username); // Replace with your From address
                mailMessage.To.Add(email);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = message;

                smtpClient.Port = 587; // Replace with your SMTP Port
                smtpClient.Host = "smtp-mail.outlook.com"; // Replace with your SMTP Host

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("justice-usms-system@outlook.com", "Justice_3141592"); // Replace with your SMTP username and password
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                await smtpClient.SendMailAsync(mailMessage);

                return;
            }
            catch (Exception)
            {
                // Log error or handle exception
                throw;
            }
        }
 

        //public EmailSender(IOptions<SendGridOptions> sendGridOptions, IOptions<BaseEmailOptions> emailOptions)
        //{
        //    SendGridOptions = sendGridOptions.Value;
        //    EmailOptions = emailOptions.Value;
        //}

        //public SendGridOptions SendGridOptions { get; }

        //public BaseEmailOptions EmailOptions { get; }

        //public Task SendEmailAsync(string email, string subject, string message) =>
        //    Execute(SendGridOptions.SendGridKey, subject, message, email);

        //public Task Execute(string apiKey, string subject, string message, string email)
        //{
        //    var client = new SendGridClient(apiKey);
        //    var msg = new SendGridMessage()

        //    {
        //        From = new EmailAddress(EmailOptions.Username, EmailOptions.Fullname),
        //        Subject = subject,
        //        PlainTextContent = message,
        //        HtmlContent = message
        //    };
        //    msg.AddTo(new EmailAddress(email));

        //    msg.SetClickTracking(false, false);

        //    return client.SendEmailAsync(msg);
        //}


    }
}
