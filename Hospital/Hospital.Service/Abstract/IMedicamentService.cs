using Hospital.Model.Entities;
using Hospital.Service.OutDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Abstract
{
   public interface IMedicamentService
    {
        Task<List<Medicament>> GetAllMedicament();
        Task<List<Medicament>> GetMedicamentByName(List<string> names);
    }
}
