using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Areas.Admin.ViewModels;
using Hospital.Core.Enums;
using Hospital.Infrastructure.Attributes;
using Hospital.Model.Identity;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.OutDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace Hospital.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Roles(SystemRoleType.Admin)]
    public class PatientController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private IRepository<Model.Entities.Patient> _patientRepository;
        private IUserRepository _userRepository;
        public PatientController(UserManager<ApplicationUser> userManager,
                              IUserService userService, IMapper mapper, IRepository<Hospital.Model.Entities.Patient> patientRepository, IUserRepository userRepository)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
            _patientRepository = patientRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> PatientDetails(string id)
        {
            var vModel = new ApplicationUserAccountDataVM();
            var user = await _userManager.FindByIdAsync(id);

            vModel = _mapper.Map<ApplicationUserAccountDataVM>(user);


            return View(vModel);
        }

        [HttpGet]
        public async Task<IActionResult> PatientEdit(string id)
        {
            var vModel = new ApplicationUserAccountDataVM();
            var user = await _userManager.FindByIdAsync(id);

            vModel = _mapper.Map<ApplicationUserAccountDataVM>(user);


            return View(vModel);
        }
        [HttpPost]
        public async Task<IActionResult> PatientEdit(ApplicationUserAccountDataVM user)
        {
            var vModel = new ApplicationUserAccountDataVM();
            bool isUserChanged = false;
            var user1= await _userManager.FindByEmailAsync(user.Email);

            vModel = _mapper.Map<ApplicationUserAccountDataVM>(user);
            
            if (user.FirstName!=user1.FirstName)
            {
                user1.FirstName = user.FirstName;
                isUserChanged = true;
            }
            if (user.LastName != user1.LastName)
            {
                user1.LastName = user.LastName;
                isUserChanged = true;
            }

            if (user.PostalCode != user1.PostalCode)
            {
                user1.PostalCode = user.PostalCode;
                isUserChanged = true;
            }
            if (user.City != user1.City)
            {
                user1.City = user.City;
                isUserChanged = true;
            }
            if (user.PESEL != user1.PESEL)
            {
                user1.PESEL = user.PESEL;
                isUserChanged = true;
            }

            if (user.Street != user1.Street)
            {
                user1.Street = user.Street;
                isUserChanged = true;
            }
            if (user.Province != user1.Province)
            {
                user1.Province = user.Province;
                isUserChanged = true;
            }
            IdentityResult result = null;
            if (isUserChanged)
            {
                 result = await _userManager.UpdateAsync(user1);
            }
            if (!result.Succeeded)
            {
                TempData["Result"] = "Błąd podczas któregoś z pól";
                return RedirectToAction("PatientBase", "Home", new { area = "Admin" });
            }

            TempData["Result"] = "Pomyślnie zmieniono dane użytkownika";
            return RedirectToAction("PatientBase", "Home", new { area = "Admin" });
        }
        [HttpGet]
        public  IActionResult PatientCreate()
        {
             return View();
        }
        [HttpPost]
        public async Task<IActionResult> PatientCreate(ApplicationUserAccountDataVM user)
        {
            var vModel = new ApplicationUserAccountDataVM();

            var dtoModel=  _mapper.Map<ApplicationUserDTO>(user);
            var appUser = _mapper.Map<ApplicationUser>(dtoModel);
            dtoModel.Password = "12345";
            dtoModel.SystemRole = SystemRoleType.Patient;
            var user1 = await _userRepository.CreateAsync(appUser, dtoModel.SystemRole, dtoModel.Password);



            if (user == null)
            {
                TempData["Result"] = "Nie udało się dodać użytkownika";
                return RedirectToAction("PatientBase", "Home", new { area = "Admin" });
            }

            var patient = new Model.Entities.Patient { User = user1 };

            await _patientRepository.InsertAsync(patient);
            TempData["Result"] = "Pomyślnie dodano użytkownika";
            return RedirectToAction("PatientBase", "Home", new { area = "Admin" });
        }
    }
}