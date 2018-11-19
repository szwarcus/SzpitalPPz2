using Hospital.Model.Entities;
using System.Threading.Tasks;

namespace Hospital.Repository.Abstract
{
    public interface IPatientRepository
    {
        Task<bool> CreateAsync(Patient model);
    }
}