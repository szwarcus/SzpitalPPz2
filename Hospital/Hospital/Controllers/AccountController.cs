using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Hospital.Model.Identity;
using Hospital.ViewModel;
using Hospital.Service.PatientServices.InDTOs;
using Hospital.Service.PatientServices.Abstract;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hospital.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;

        private IPatientAccountService _patientAccountService;

        public AccountController(SignInManager<ApplicationUser> signInManager, 
                                 UserManager<ApplicationUser> userManager,
                                 RoleManager<ApplicationIdentityRole> roleManager,
                                 IPatientAccountService patientAccountService
                                 )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

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
                    return RedirectToAction("Index", "Home");
                }
            }

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

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout( )
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}