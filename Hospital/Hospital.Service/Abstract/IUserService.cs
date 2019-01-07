using System.Collections.Generic;
using System.Threading.Tasks;
using Hospital.Model.Identity;
using Hospital.Service.OutDTOs;

namespace Hospital.Service.Abstract
{
    public interface IUserService
    {
       Task<List<ApplicationUserDTO>> GetAllUsersByRole(string roleName);
    }
}