using System.Threading.Tasks;
using Hospital.Model.Identity;

namespace Hospital.Repository.Abstract
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(ApplicationUser applicationUser, string password);
    }
}