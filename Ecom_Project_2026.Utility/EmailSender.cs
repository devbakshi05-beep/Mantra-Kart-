using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Project_2026.Utility
{
    public class EmailSender : IEmailSender
    {
        private EmailSettings _emailSettings { get; }
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task Execute(string email, string subject, string message)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG >> Domain:[{_emailSettings.PrimaryDomain}] Port:[{_emailSettings.PrimaryPort}] User:[{_emailSettings.UsernameEmail}] PassLength:[{_emailSettings.UsernamePassword?.Length}]");

            try
            {
                string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.FromEmail : email;
                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "My Email Name")
                };
                mailMessage.To.Add(toEmail);
                if (!string.IsNullOrEmpty(_emailSettings.CcEmail))
                {
                    mailMessage.CC.Add(_emailSettings.CcEmail);
                }
                //mailMessage.CC.Add(_emailSettings.CcEmail);
                mailMessage.Subject = "Shopping App : " + subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;

                //    using (SmtpClient smtpClient = new SmtpClient(_emailSettings.PrimaryDomain,
                //        _emailSettings.PrimaryPort))
                //    {
                //        smtpClient.UseDefaultCredentials = false;   // 👈 Yaha add kiya
                //        smtpClient.Credentials = new NetworkCredential
                //            (_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                //        smtpClient.EnableSsl = true;
                //        await smtpClient.SendMailAsync(mailMessage);
                //    }
                //}
                using (SmtpClient smtpClient = new SmtpClient(
        _emailSettings.PrimaryDomain,
        Convert.ToInt32(_emailSettings.PrimaryPort)))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(
                        _emailSettings.UsernameEmail,
                        _emailSettings.UsernamePassword
                    );
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Execute(email, subject, htmlMessage);  // ✅ Proper async
        }

    }

}
