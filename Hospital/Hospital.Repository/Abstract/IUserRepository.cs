using System.Threading.Tasks;
using Hospital.Model.Identity;

namespace Hospital.Repository.Abstract
{
    public interface IUserRepository
    {
        Task<ApplicationUser> CreateAsync(ApplicationUser applicationUser, string password);
    }
}