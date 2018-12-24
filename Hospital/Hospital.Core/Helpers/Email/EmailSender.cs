using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Hospital.Core.Helpers.Email
{
    public class EmailSender: IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendEmailAsync(string email, string message, string subject)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential() {
                    UserName = _configuration["Email:Email"],
                    Password = _configuration["Email:Password"]
                    
                };
                client.Credentials = credential;
                client.Host = _configuration["Email:Host"];
                client.Port = int.Parse(_configuration["Email:Port"]);
                client.EnableSsl = true;

                var emailMesage = new MailMessage();
                emailMesage.To.Add(email);
                emailMesage.From = new MailAddress (_configuration["Email:Email"]);
                emailMesage.Subject = subject;
                emailMesage.Body = message;
                await client.SendMailAsync(emailMesage);
                
            }
            return true;
        }
    }
}