using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Hospital.Core.Helpers.Email
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public SendGridEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendEmailAsync(string email, string message, string subject)
        {
            var apiKey = _configuration["SendGrid:SendGridKey"];
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(_configuration["SendGrid:EmailFrom"]);
            var to = new EmailAddress(email);
            var fullMessage = MailHelper.CreateSingleEmail(from,to, subject, message,"");

            var response = await client.SendEmailAsync(fullMessage);

            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}
