using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Areas.Doctor.ViewModels;
using Hospital.Core.Enums;
using Hospital.Infrastructure.Attributes;
using Hospital.Model.Entities;
using Hospital.Model.Identity;
using Hospital.Service.Abstract;
using Hospital.Service.InDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    [Roles(SystemRoleType.Doctor)]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDoctorService _doctorService;
        private readonly IMedicamentService _medicamentService;
        private readonly IPrescriptionService _prescriptionService;
        private readonly IPatientAccountService _patientAccountService;
        private readonly IVisitService _visitService;
        
        public HomeController(UserManager<ApplicationUser> userManager, IDoctorService doctorService, IMedicamentService medicamentService, IPrescriptionService prescriptionService, IPatientAccountService patientAccountService, IVisitService visitService)
        {
            _userManager = userManager;
            _doctorService = doctorService;
            _medicamentService = medicamentService;
            _prescriptionService = prescriptionService;
            _patientAccountService = patientAccountService;
            _visitService = visitService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> TimeTable()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var doctor = await _doctorService.GetDoctorById(user.Id);
            var visits = await _doctorService.GetAllDoctorVisitsByDoctorID(doctor.Id);
            return View(visits);
        }
        [HttpGet]
        public async Task<IActionResult> CurrentVisit()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var currentVisit = await _doctorService.GetCurrnetVisitByDoctorID(user.Id);
            var allMedicament = await _medicamentService.GetAllMedicament();
            if(currentVisit == null)
                return RedirectToAction("TimeTable", "Home", new { area = "Doctor" });
            var patient = await _patientAccountService.GetPatientById(currentVisit.PatientId);

            CurrentVisitVM viewModel = new CurrentVisitVM()
            {
                Medicaments = allMedicament,
                FirstName = patient.FirstName,
                LastName = patient.LastName
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CurrentVisit(CurrentVisitVM vm)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var currentVisit = await _doctorService.GetCurrnetVisitByDoctorID(user.Id);
            var medicament = await _medicamentService.GetMedicamentByName(vm.selectedMedicaments);

            var prescriptionInDTO = new PrescriptionInDTO()
            {
                Comments = vm.DescriptionPrescription,
                DueDate = vm.DueDate,
                VisitId = currentVisit.Id,
                Medicaments = medicament
            };
            await _prescriptionService.Create(prescriptionInDTO);
            await _visitService.UpdateVisit(new UpdateVisitInDTO() { Description = vm.DescriptionVisit, Id = currentVisit.Id });

            return View("Index");
        }
    }
}