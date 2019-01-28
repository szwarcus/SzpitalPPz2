using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Core.Enums;
using Hospital.Infrastructure.Attributes;
using Hospital.Model.Entities;
using Hospital.Model.Identity;
using Hospital.Service.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    [Roles(SystemRoleType.Doctor)]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDoctorService _doctorService;
        public HomeController(UserManager<ApplicationUser> userManager, IDoctorService doctorService)
        {
            _userManager = userManager;
            _doctorService = doctorService;
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
    }
}