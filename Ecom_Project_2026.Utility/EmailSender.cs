using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecom_Project_2026.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private static readonly HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(20)   // 20 second se zyada wait nahi karega
        };

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG >> SendEmailAsync STARTED");
            string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.FromEmail : email;

            var payload = new
            {
                sender = new { name = "Shopping App", email = _emailSettings.FromEmail },
                to = new[] { new { email = toEmail } },
                subject = "Shopping App : " + subject,
                htmlContent = htmlMessage
            };

            var json = JsonSerializer.Serialize(payload);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.brevo.com/v3/smtp/email");
            request.Headers.Add("api-key", _emailSettings.BrevoApiKey);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Brevo email failed: {response.StatusCode} - {responseBody}");
            }
        }
    }
}