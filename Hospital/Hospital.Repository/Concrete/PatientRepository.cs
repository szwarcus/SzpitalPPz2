using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Hospital.Model.Entities;
using Hospital.Repository.Abstract;

namespace Hospital.Repository.Concrete
{
    public class PatientRepository : BaseRepository, IPatientRepository
    {
        public async Task<bool> CreateAsync(Patient model)
        {
            try
            {
                var x = await _context.Patients.AddAsync(model);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
    }
}