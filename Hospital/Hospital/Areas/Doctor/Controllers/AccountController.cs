using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Areas.Doctor.ViewModels;
using Hospital.Core.Enums;
using Hospital.Core.Helpers.Email;
using Hospital.Infrastructure.Attributes;
using Hospital.Model.Identity;
using Hospital.Service.Abstract;
using Hospital.Service.InDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    [Roles(SystemRoleType.Doctor)]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;

        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<ApplicationIdentityRole> roleManager, IMapper mapper, IEmailSender emailSender, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

            _emailSender = emailSender;
            _mapper = mapper;
            _userService = userService;

        }
        [HttpGet]
        public async Task<IActionResult> Account()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            var model = new AccountDataVM()
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PESEL = user.PESEL,
                Phone = user.PhoneNumber,
                City = user.City,
                Street = user.Street,
                PostalCode = user.PostalCode,
                Province = user.Province

            };
            return View(model);
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResultAction()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ChangeEmail()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AcceptChangeEmail(string userId, string token, string newEmail)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["Result"] = "Błąd podczas zmiany emaila";
                return RedirectToAction("ResultAction", "Account", new { area = "Doctor" });
            }
                

            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
            await _userManager.UpdateNormalizedEmailAsync(user);
            if (!result.Succeeded)
            {
                TempData["Result"] = "Błąd podczas zmiany emaila";
                return RedirectToAction("ResultAction", "Account", new { area = "Doctor" });
            }

            TempData["Result"] = "Pomyślnie zmieniono emaila";
            return RedirectToAction("ResultAction", "Account", new { area = "Doctor" });

        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var inDTOmodel = _mapper.Map<ChangePasswordInDTO>(model);
            inDTOmodel.UserId = user.Id;
            var result = await _userService.ChangePassword(inDTOmodel);

            if (result) {
                TempData["Result"]= "Pomyślnie zmieniono hasło!";
                return RedirectToAction("ResultAction", "Account", new { area = "Doctor" });
            }
            else return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangeEmail(ChangeEmailVM model)
        {
            if (!ModelState.IsValid)
                return View();
            //TODO: chyba inaczej trzeba sprawdzać czy email jest zajęty
            var user1 = await _userManager.FindByEmailAsync(model.NewEmail);
            if (user1 != null)
            {
                ModelState.AddModelError(nameof(model.NewEmail), "Podany adres jest już używany");
                return View();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var correctPassword = await _userManager.CheckPasswordAsync(user,model.Password);
            if (correctPassword == false)
            {
                ModelState.AddModelError(nameof(model.Password), "Nieprawidłowe hasło");
                return View();
            }
           
            var tokeN = await _userManager.GenerateChangeEmailTokenAsync(user,model.NewEmail);
            var link = Url.Action("AcceptChangeEmail", "Account", new { userId = user.Id, token = tokeN, newEmail=model.NewEmail }, protocol: HttpContext.Request.Scheme);
            var res = await _emailSender.SendEmailAsync(model.NewEmail, link, "Confirm Change Email");

            if (res) {
                TempData["Result"] = "Wysłano wiadomość z linkiem potwierdzającym zmiane emaila";
                return RedirectToAction("ResultAction", "Account", new { area = "Doctor" });
            }
            else
                return View();
        }
    

    }
}