using System.Threading.Tasks;
using AutoMapper;
using Hospital.Areas.Admin.ViewModels;
using Hospital.Core.Enums;
using Hospital.Infrastructure.Attributes;
using Hospital.Model.Identity;
using Hospital.Service.Abstract;
using Hospital.Service.InDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Roles(SystemRoleType.Admin)]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVisitService _visitService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager,
                              IMapper mapper,
                              IVisitService visitService,
                              IDoctorService doctorService,
                              IPatientService patientService)
        {
            _visitService = visitService;
            _doctorService = doctorService;
            _patientService = patientService;
            _mapper = mapper;

            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var vModel = new HomeVM();

            var activeDoctors = await _doctorService.GetAllActiveDoctors();
            var activePatients = await _patientService.GetAllActivePatients();
            
            vModel.StatisticsVM = new StatisticsVM();
            vModel.StatisticsVM.statisticsDictionary.Add("Ilość doktorów", activeDoctors.Count);
            vModel.StatisticsVM.statisticsDictionary.Add("Ilość pacjentów", activePatients.Count);

            return View(vModel);
        }


    }   
}