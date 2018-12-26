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

            activeDoctors.ForEach(doctor => vModel.Doctors.Add(new SelectListItem
            {
                Value = doctor.DoctorId.ToString(),
                Text = $"{doctor.FirstName} {doctor.LastName}"
            }));

            return View(vModel);
        }
     
        public async Task<IActionResult> ArrangeVisit(ArrangeVisitVM model)
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

            await _visitService.ArrangeVisit(dtoModel);

            return RedirectToAction("Index", "Home", new { area = "Patient" });
        }
    }
}