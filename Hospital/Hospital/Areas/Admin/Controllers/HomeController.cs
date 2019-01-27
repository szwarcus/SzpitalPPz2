using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Areas.Admin.ViewModels;
using Hospital.Core.Enums;
using Hospital.Infrastructure.Attributes;
using Hospital.Model.Identity;
using Hospital.Service.Abstract;
using Hospital.Service.InDTOs;
using Hospital.Service.OutDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Roles(SystemRoleType.Admin)]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager,
                              IUserService userService)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var vModel = new HomeVM();

            var activePatients = await _userService.GetAllUsersByRole("PATIENT");
            var activeDoctors = await _userService.GetAllUsersByRole("DOCTOR");
            var activeNurses = await _userService.GetAllUsersByRole("NURSE");
            vModel.StatisticsVM = new StatisticsVM();
            vModel.StatisticsVM.statisticsDictionary.Add("Doctor", activeDoctors.Count);
            vModel.StatisticsVM.statisticsDictionary.Add("Patient", activePatients.Count);
            vModel.StatisticsVM.statisticsDictionary.Add("Nurse", activeNurses.Count);

            return View(vModel);
        }

        public async Task<IActionResult> PatientBase()
        {
            var vModel = new HomeVM();

            var activePatients = await _userService.GetAllUsersByRole("PATIENT");
            vModel.applicationUsers = new List<ApplicationUserDTO>();
            activePatients.ForEach(patient => vModel.applicationUsers.Add(patient));
            return View(vModel);
        }




    }   
}