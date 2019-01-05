using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Hospital.Model.Identity;
using Hospital.ViewModel;
using Hospital.Service.InDTOs;
using Hospital.Service.Abstract;
using AutoMapper;
using Hospital.Core.Helpers.Email;
using Hospital.Core.Enums;
using System.Security.Claims;
using System.Linq;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hospital.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;

        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        private IPatientAccountService _patientAccountService;

        public AccountController(SignInManager<ApplicationUser> signInManager, 
                                 UserManager<ApplicationUser> userManager,
                                 RoleManager<ApplicationIdentityRole> roleManager,
                                 IMapper mapper,
                                 IPatientAccountService patientAccountService,
                                 IEmailSender emailSender
                                 )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

            _emailSender = emailSender;
            _mapper = mapper;
            _patientAccountService = patientAccountService;
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
        public IActionResult ResetPassword(string token, string userId)
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

        [HttpGet]
        public async Task<string> GetCurrentUserId()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return user?.Id;
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

            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError(nameof(model.Email), "Email or password incorrect"); // result to change
                    }
                    else if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(nameof(model.Email), "Not Email Confirmed");
                    }
                    else
                    {
                        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

                        var roles = claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Role)
                                                          .Select(c => c.Value)
                                                          .ToList();

                        if (roles != null && roles.Count > 0)
                        {
                            var role = roles.FirstOrDefault();

                            switch (role)
                            {
                                case nameof(SystemRoleType.Patient):
                                    return RedirectToAction("Index", "Home", new { area = "Patient" });
                                case nameof(SystemRoleType.Doctor):
                                    break;
                                case nameof(SystemRoleType.Nurse):
                                    break;
                                case nameof(SystemRoleType.Admin):
                                    // nie wiem jakie zachowanie, admin powinien inna stronka sie logowac, wiec jakis error raczej
                                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                                    //ModelState.AddModelError(nameof(model.Email), "Your account is admin account"); // do zmiany
                                    break;
                                default:
                                    return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Password), "Nieprawidłowe hasło");
                }
            }
            else
            {
                ModelState.AddModelError(nameof(model.Email), "Nie ma użytkownika o podanym emailu");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPatient(RegisterPatientVM model)
        {
            // dodac info ze rejestracja sie nie powiodla

            if (!ModelState.IsValid)
            {
                return View();
            }

            var dtoModel = _mapper.Map<RegisterPatientInDTO>(model);
            dtoModel.SystemRole = SystemRoleType.Patient;

            var result = await _patientAccountService.Register(dtoModel);

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
        public async Task<IActionResult> SendPasswordResetLink(ForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("ForgotPassword");
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(nameof(model.Email), "Nie ma użytkownika o podanym emailu");
                return View("ForgotPassword");
            }

            var tokeN = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new ResetPasswordVM { UserId = user.Id, Token = tokeN }, protocol: HttpContext.Request.Scheme);
            var res = await _emailSender.SendEmailAsync(user.Email, link, "Reset Password");
            if (!res)
                return View("ForgotPassword");

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {

            if (model.Password != model.ConfirmPassword)
                ModelState.AddModelError(nameof(model.ConfirmPassword), "Podane hasła są różne!");

            if (!ModelState.IsValid)
                return View();

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
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