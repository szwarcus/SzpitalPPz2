using Hospital.Service.InDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Abstract
{
   public interface IPrescriptionService
    {
        Task Create(PrescriptionInDTO prescription); 
    }
}
