using System.Collections.Generic;
using System.Threading.Tasks;
using Hospital.Service.OutDTOs;

namespace Hospital.Service.Abstract
{
    public interface ISpecializationService
    {
        Task<ICollection<SpecializationOutDto>> GetAllAsync();
        Task<ICollection<SpecializationOutDto>> GetByNameAsync(string specializationName);
    }
}