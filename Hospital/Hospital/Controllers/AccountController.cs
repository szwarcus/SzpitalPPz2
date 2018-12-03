using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Hospital.Model.Identity;
using Hospital.ViewModel;
using Hospital.Service.PatientServices.InDTOs;
using Hospital.Service.PatientServices.Abstract;
using Hospital.Service.Helpers.Email;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hospital.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;

        private IPatientAccountService _patientAccountService;
        private readonly IEmailSender _emailSender;

        public AccountController(SignInManager<ApplicationUser> signInManager, 
                                 UserManager<ApplicationUser> userManager,
                                 RoleManager<ApplicationIdentityRole> roleManager,
                                 IPatientAccountService patientAccountService,
                                 IEmailSender emailSender
                                 )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

            _patientAccountService = patientAccountService;
            _emailSender = emailSender;
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
         
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
                return View("Error");
            if( await _userManager.IsEmailConfirmedAsync(user) == true) /// trzeba wyswietlic informacje ze mail został już potwierdzony
                return View("Error");
            var result = await _patientAccountService.ConfirmEmail(new ConfirmEmailPatientInDTO() { user = user, token = token });

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

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                   if(await _userManager.IsEmailConfirmedAsync(user))
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

            var result = await _patientAccountService.Register(new RegisterPatientInDTO
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Nick,
                PhoneNumber = model.Phone,
                SystemRole = Role.Patient,
                Password = model.Password
            });

            if (!result)
            {
                // nie udalo sie
                return View();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            var tokeN = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token=tokeN },protocol:HttpContext.Request.Scheme);
            var res = await _emailSender.SendEmailAsync(user.Email, link, "Confirm");
            if(!res)
                return View();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout( )
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SendPasswordResetLink(string email)
        {
            if (email == "")
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View();

            var tokeN = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = tokeN }, protocol: HttpContext.Request.Scheme);
            var res = await _emailSender.SendEmailAsync(user.Email, link, "Reset Password");
            if (!res)
                return View();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return View("Error");
            if (model.Password != model.ConfirmPassword)
                return View("Error");

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");

        }
        #endregion
    }
}