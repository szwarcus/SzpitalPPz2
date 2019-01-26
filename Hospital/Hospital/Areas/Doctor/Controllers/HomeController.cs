using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Core.Enums;
using Hospital.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    [Roles(SystemRoleType.Doctor)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}