using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Areas.Admin.ViewModels;
using Hospital.Core.Enums;
using Hospital.Infrastructure.Attributes;
using Hospital.Model.Identity;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.OutDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace Hospital.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Roles(SystemRoleType.Admin)]
    public class MedicamentController : Controller
    {

        private IRepository<Hospital.Model.Entities.Medicament> _medicamentRepository;
        public MedicamentController(IRepository<Hospital.Model.Entities.Medicament> medicamentRepository)
        {
            _medicamentRepository = medicamentRepository;
        }

      
        [HttpGet]
        public  IActionResult MedicamentCreate()
        {
             return View();
        }
      
        [HttpGet]
        public async Task<IActionResult> MedicamentDelete(string id)
        {
            var vModel = new MedicamentVM();
            //var user = await _userManager.FindByIdAsync(id);
            var medicamentsToDelete = await _medicamentRepository.GetAsync(medicament1 => medicament1, medicament1 => medicament1.Id.ToString() == id);
            //var doctorsToDelete = await _patientRepository.GetAsync(user1 => user1, user1 => user1.UserId == user.Id);


            var medicament = medicamentsToDelete.FirstOrDefault();

            await _medicamentRepository.DeleteAsync(medicament);
            
            return RedirectToAction("MedicamentBase", "Home", new { area = "Admin" });
        }
    }
}