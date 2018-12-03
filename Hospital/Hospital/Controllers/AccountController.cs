using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Hospital.Model.Identity;
using Hospital.ViewModel;
using Hospital.Service.PatientServices.InDTOs;
using Hospital.Service.PatientServices.Abstract;
using AutoMapper;
using Hospital.Service.Helpers.Email;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hospital.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationIdentityRole> roleManager;

        private readonly IMapper mapper;
        private readonly IEmailSender _emailSender;

        private IPatientAccountService patientAccountService;

        public AccountController(SignInManager<ApplicationUser> signInManager, 
                                 UserManager<ApplicationUser> userManager,
                                 RoleManager<ApplicationIdentityRole> roleManager,
                                 IMapper mapper,
                                 IPatientAccountService patientAccountService
                                 IEmailSender emailSender
                                 )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.patientAccountService = patientAccountService;
        }

        #region GetMethods
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Authenticated()
        {
            // nie wiem czy sie przyda, pewnie nie, ale na razie mogloby zostac jako przyklad
            return Ok(new UserStateVM
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                Username = User.Identity.IsAuthenticated ? User.Identity.Name : string.Empty
            });
        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail( string userId , string token)
        {
         
            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
                return View("Error");
            if( await userManager.IsEmailConfirmedAsync(user) == true) /// trzeba wyswietlic informacje ze mail został już potwierdzony
                return View("Error");
            var result = await patientAccountService.ConfirmEmail(new ConfirmEmailPatientInDTO() { user = user, token = token });

            return View(result ? "ConfirmEmail" : "Error");

        }
        #endregion

        #region PostMethods
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            // dodac jakos ze logowanie sie nie udalo

            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    // if doctor
                    
                    // if patient
                    
                    // if nurse
                   if(await userManager.IsEmailConfirmedAsync(user))
                    return RedirectToAction("Index", "Home");
                   else
                        ModelState.AddModelError("", "Not Email Confirmed");

                }
            }
            else
                 ModelState.AddModelError("", "User name or password not found");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            // dodac info ze rejestracja sie nie powiodla

            if (!ModelState.IsValid)
            {
                return View();
            }

            var dtoModel = mapper.Map<RegisterPatientInDTO>(model);
            dtoModel.SystemRole = Role.Patient;

            var result = await patientAccountService.Register(dtoModel);

            if (!result)
            {
                // nie udalo sie
                return View();
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            var tokeN = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token=tokeN },protocol:HttpContext.Request.Scheme);
            var res = await _emailSender.SendEmailAsync(user.Email, link, "Confirm");
            if(!res)
                return View();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout( )
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SendPasswordResetLink(string email)
        {
            if (email == "")
            {
                return View();
            }
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return View();

            var tokeN = await userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = tokeN }, protocol: HttpContext.Request.Scheme);
            var res = await _emailSender.SendEmailAsync(user.Email, link, "Reset Password");
            if (!res)
                return View();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return View("Error");
            if (model.Password != model.ConfirmPassword)
                return View("Error");

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");

        }
        #endregion
    }
}