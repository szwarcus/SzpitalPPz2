using Hospital.Model.Entities;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.OutDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Concrete
{
   public class MedicamentService : IMedicamentService
    {
        IRepository<Medicament> _repository;

        public MedicamentService(IRepository<Medicament> repository)
        {
            _repository = repository;
        }
        public async Task<List<Medicament>> GetAllMedicament()
        {
            return await _repository.GetAllAsync(x => x);
        }

        public async Task<List<Medicament>> GetMedicamentByName(List<string> names)
        {
            List<Medicament> medicaments = new List<Medicament>();
            foreach (var item in names)
            {
                var one = await _repository.GetAsync(x => x, x => x.Name == item);
                medicaments.Add(one.FirstOrDefault());
            }
            return medicaments;
        }
    }
}
