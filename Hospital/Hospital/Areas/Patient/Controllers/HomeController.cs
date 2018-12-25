using System;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Areas.Patient.ViewModels;
using Hospital.Core.Enums;
using Hospital.Infrastructure.Attributes;
using Hospital.Model.Identity;
using Hospital.Service.Abstract;
using Hospital.Service.InDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Roles(SystemRoleType.Patient)]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVisitService _visitService;
        private readonly IDoctorService _doctorService;

        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager,
                              IMapper mapper,
                              IVisitService visitService,
                              IDoctorService doctorService)
        {
            _visitService = visitService;
            _doctorService = doctorService;
            _mapper = mapper;

            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var vModel = new HomeVM();

            var activeDoctors = await _doctorService.GetAllActiveDoctors();

            // przerzucic do automappera
            activeDoctors.ForEach(doctor => vModel.Doctors.Add(new DoctorVM { DoctorUserId = doctor.UserId }));

            return View(vModel);
        }
     
        public async Task<IActionResult> ArrangeVisit(ArrangeVisitVM model)
        {
            if (!ModelState.IsValid || String.IsNullOrEmpty(model.DoctorUserId))
            {
                return RedirectToAction("Index", "Home"); ;
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // dodac to do automappera, zeby mapowalo vm do dto
            var dtoModel = new ArrangeVisitInDTO
            {
                DoctorUserId = model.DoctorUserId,
                PatientUserId = user.Id,
                Date = model.VisitDate,
                Description = model.Description
            };

            await _visitService.ArrangeVisit(dtoModel);

            return RedirectToAction("Index", "Home", new { area = "Patient" });
        }
    }
}