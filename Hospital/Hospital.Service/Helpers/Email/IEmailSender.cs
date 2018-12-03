using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Helpers.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string email, string message, string subject);
    }
}
