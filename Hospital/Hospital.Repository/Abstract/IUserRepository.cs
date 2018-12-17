using System.Threading.Tasks;
using Hospital.Core.Enums;
using Hospital.Model.Identity;

namespace Hospital.Repository.Abstract
{
    public interface IUserRepository
    {
        Task<ApplicationUser> CreateAsync(ApplicationUser applicationUser, SystemRoleType role, string password);
        Task<ApplicationUser> FindAsync(string id);
        Task<bool> ConfirmEmailAsync(ApplicationUser applicationUser, string token);
    }
}