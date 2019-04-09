using Hospital.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hospital.Service.OutDTOs;

namespace Hospital.Service.Abstract
{
   public interface IMedicamentService
    {
        Task<List<Medicament>> GetAllMedicament();
        Task<List<Medicament>> GetMedicamentByName(List<string> names);
        Task<List<Medicament>> GetMedicamentsByLetter(char page);
        Task<int> CountAllMedicaments();
    }
}