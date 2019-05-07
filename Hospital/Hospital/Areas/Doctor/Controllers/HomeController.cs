using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentDateTime;
using Hospital.Areas.Doctor.ViewModels;
using Hospital.Core.Enums;
using Hospital.Infrastructure.Attributes;
using Hospital.Model.Entities;
using Hospital.Model.Identity;
using Hospital.Service.Abstract;
using Hospital.Service.InDTOs;
using Hospital.Service.OutDTOs;
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
        private readonly ISpecializationService _specializationService;

        public HomeController(UserManager<ApplicationUser> userManager, IDoctorService doctorService, IMedicamentService medicamentService, IPrescriptionService prescriptionService, IPatientAccountService patientAccountService, IVisitService visitService, ISpecializationService specializationService)
        {
            _userManager = userManager;
            _doctorService = doctorService;
            _medicamentService = medicamentService;
            _prescriptionService = prescriptionService;
            _patientAccountService = patientAccountService;
            _visitService = visitService;
            _specializationService = specializationService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> TimeTable(string dateTime)
        {
            DateTime dateTime1;
            DateTime.TryParse(dateTime,out dateTime1);
            if (dateTime1 == null)
                return RedirectToAction("TimeTable", "Home", new { area = "Doctor" });

            DateTime StartTime = dateTime1.DayOfWeek == DayOfWeek.Monday ? dateTime1.SetHour(8).SetMinute(0).SetSecond(0) : dateTime1.Previous(DayOfWeek.Monday).SetHour(8).SetMinute(0).SetSecond(0);

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var doctor = await _doctorService.GetDoctorById(user.Id);
            var visits = await _doctorService.GetAllDoctorVisitsByDoctorID(doctor.Id);
            var result = visits.Where(x => x.Date >= StartTime && x.Date < StartTime.AddDays(7)).ToList();
            var dto = new TimeTableVM()
            {
                DateTime = StartTime,
                Visits = result.Select(x => new CurrentVisitOutDTO() { Id = x.Id, State = x.State, DateTime = x.Date }).ToList()
            };
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> VisitDetails(string date, long patientID)
        {
            DateTime dateTime ;
            DateTime.TryParse(date,out dateTime);
            if(dateTime == null)
                return Json(null);
            var result = await _visitService.GetVisitDetails(patientID, dateTime);
            return Json(result);
        }
        public async Task<IActionResult> CurrentVisit(long VisitId)
        {
            var currentVisit = await _visitService.GetById(VisitId);
            if(currentVisit == null || currentVisit.State == StateVisit.Completed)
                return RedirectToAction("TimeTable", "Home", new { area = "Doctor" });

            var allMedicament = await _medicamentService.GetAllMedicament();

            var patient = await _patientAccountService.GetPatientById(currentVisit.PatientId);

            var visits = await _visitService.GetCompletedVisits(currentVisit.PatientId);
            visits.Reverse();

   

            CurrentVisitVM viewModel = new CurrentVisitVM()
            {
                Medicaments = allMedicament,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Birth = patient.Birth,
                VisitID = currentVisit.Id,
                State = currentVisit.State,
                Visits = visits,
                PatientID = patient.UserID
                              
            };
            var specializationOutDto = await _specializationService.GetAllAsync();
            specializationOutDto.Select(x => x.Name).ToList().ForEach(name =>
            {
                viewModel.Specializations.Add(new SelectListItem
                {
                    Text = name,
                    Value = name
                });
            });

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CurrentVisit(CurrentVisitVM vm)
        {
            if(vm.State == StateVisit.Completed)
                return RedirectToAction("TimeTable", "Home", new { area = "Doctor" });
            var medicament = await _medicamentService.GetMedicamentByName(vm.selectedMedicaments);

            var prescriptionInDTO = new PrescriptionInDTO()
            {
                Comments = vm.DescriptionPrescription,
                VisitId = vm.VisitID,
                Medicaments = medicament
            };
            await _prescriptionService.Create(prescriptionInDTO);
            await _visitService.UpdateVisit(new UpdateVisitInDTO() { Description = vm.DescriptionVisit, Id = vm.VisitID, State = StateVisit.Completed });

            return View("Index");
        }
    }
}