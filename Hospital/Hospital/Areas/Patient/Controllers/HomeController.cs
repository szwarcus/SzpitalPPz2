using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Core.Enums;
using Hospital.Infrastructure.Attributes;
using Hospital.Model.Entities;
using Hospital.Model.Identity;
using Hospital.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Roles(SystemRoleType.Patient)]
    public class HomeController : Controller
    {    
        public IActionResult Index()
        {
            return View();
        }
     
    }
}