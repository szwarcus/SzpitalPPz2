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
        private readonly IMedicamentService _medicamentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public HomeController(UserManager<ApplicationUser> userManager,
                              IUserService userService,IMedicamentService medicamentService, IMapper mapper)
        {
            _userService = userService;
            _userManager = userManager;
            _medicamentService = medicamentService;
            _mapper = mapper;
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
            vModel.applicationUsersDTO = new List<ApplicationUserDTO>();
            activePatients.ForEach(patient => vModel.applicationUsersDTO.Add(patient));
            return View(vModel);
        }

        public async Task<IActionResult> DoctorBase()
        {
            var vModel = new HomeVM();

            var activeDoctors = await _userService.GetAllUsersByRole("DOCTOR");
            vModel.applicationUsersDTO = new List<ApplicationUserDTO>();
            activeDoctors.ForEach(doctor => vModel.applicationUsersDTO.Add(doctor));
            return View(vModel);
        }

        public async Task<IActionResult> MedicamentBase()
        {

            var vModel = new HomeVM();

            var medicaments = await _medicamentService.GetAllAsync();
            vModel.medicamentsVM = new List<MedicamentVM>();
            foreach (var med in medicaments)
             {
                vModel.medicamentsVM.Add(_mapper.Map<MedicamentVM>(med));
            }

            return View(vModel);

        }

        public async Task<IActionResult> Delete(string id)
        {
            var vModel = new HomeVM();
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return View(vModel);
        }
        
   


    }
}