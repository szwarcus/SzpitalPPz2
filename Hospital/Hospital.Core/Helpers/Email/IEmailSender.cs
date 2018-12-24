using System.Threading.Tasks;

namespace Hospital.Core.Helpers.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string email, string message, string subject);
    }
}