using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hospital.Models;
using Hospital.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Hospital.Model.Identity;

namespace Hospital.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public HomeController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User)) {

               if(User.IsInRole(nameof(SystemRoleType.Doctor)))
                     return RedirectToAction("Index", "Home", new { area = "Doctor" });
               if(User.IsInRole(nameof(SystemRoleType.Patient)))
                    return RedirectToAction("Index", "Home", new { area = "Patient" });
                if (User.IsInRole(nameof(SystemRoleType.Admin)))
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            
            return View();
        }
        public IActionResult Interna()
        {
            
            return View();
        }
        public IActionResult Surgery()
        {

            return View();
        }
        public IActionResult Opthamology()
        {

            return View();
        }
        public IActionResult Allergology()
        {

            return View();
        }
        public IActionResult Dentist()
        {

            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
