using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using brevo_csharp.Api;
using brevo_csharp.Client;
using brevo_csharp.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Ecom_Project_2026.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async System.Threading.Tasks.Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.FromEmail : email;

                brevo_csharp.Client.Configuration.Default.ApiKey.Add("api-key", _emailSettings.BrevoApiKey);

                var apiInstance = new TransactionalEmailsApi();

                var sender = new SendSmtpEmailSender("Shopping App", _emailSettings.FromEmail);

                var toList = new List<SendSmtpEmailTo>
                {
                    new SendSmtpEmailTo(toEmail)
                };

                if (!string.IsNullOrEmpty(_emailSettings.CcEmail))
                {
                    // Brevo mein CC ke liye "cc" parameter alag se pass hota hai
                }

                var sendSmtpEmail = new SendSmtpEmail(
                    sender: sender,
                    to: toList,
                    subject: "Shopping App : " + subject,
                    htmlContent: htmlMessage
                );

                await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}