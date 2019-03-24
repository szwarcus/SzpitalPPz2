using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Areas.Patient.ViewModels;
using Hospital.Core.Enums;
using Hospital.Infrastructure.Attributes;
using Hospital.Model.Identity;
using Hospital.Service.Abstract;
using Hospital.Service.InDTOs;
using Hospital.Service.OutDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Roles(SystemRoleType.Patient)]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVisitService _visitService;
        private readonly IDoctorService _doctorService;
        private readonly ISpecializationService _specializationService;

        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager,
                              IMapper mapper,
                              IVisitService visitService,
                              IDoctorService doctorService,
                              ISpecializationService specializationService)
        {
            _visitService = visitService;
            _doctorService = doctorService;
            _specializationService = specializationService;
            _mapper = mapper;

            _userManager = userManager;
        }

        #region Get Methods
        public async Task<IActionResult> Index()
        {
            var vModel = new HomeIndexVM();

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return View(vModel);
            }

            var visits = await _visitService.GetBaseInfoVisitsInPastAndNext30DaysAsync(user.Id);
            vModel.Visits = _mapper.Map<BaseInfoPastAndLastVisitsVM>(visits);

            var specializationOutDto = await _specializationService.GetAllAsync();
            specializationOutDto.Select(x => x.Name).ToList().ForEach(name =>
            {
                vModel.ArrangeVisit.Specializations.Add(new SelectListItem
                {
                    Text = name,
                    Value = name
                });
            });

            return View(vModel);
        }

        public async Task<IActionResult> Doctors(string day, string specializationName)
        {
            if (!ModelState.IsValid || String.IsNullOrEmpty(day) || String.IsNullOrEmpty(specializationName))
            {
                return Json(null);
            }

            var result = await _doctorService.GetActiveDoctorsByDayAndSpecializationAsync(specializationName, day, "yyyy-MM-dd");

            return Json(result);
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Creating visit by patient
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Patient/Home/Index</returns>
        [HttpPost]
        public async Task<IActionResult> Visit(CreateVisitVM model)
        {
            if (!ModelState.IsValid || model.DoctorId < 1)
            {
                return RedirectToAction("Index", "Home"); ;
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var dtoModel = _mapper.Map<ArrangeVisitInDTO>(model);
            dtoModel.PatientUserId = user.Id;
            dtoModel.Description = dtoModel.Description ?? "";

            await _visitService.ArrangeVisit(dtoModel);

            return RedirectToAction("Index", "Home", new { area = "Patient" });
        }
        #endregion
    }
}