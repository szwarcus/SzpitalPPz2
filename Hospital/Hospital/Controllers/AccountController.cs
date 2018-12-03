using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Hospital.Model.Identity;
using Hospital.ViewModel;
using Hospital.Service.PatientServices.InDTOs;
using Hospital.Service.PatientServices.Abstract;
using AutoMapper;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hospital.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationIdentityRole> roleManager;

        private readonly IMapper mapper;

        private IPatientAccountService patientAccountService;

        public AccountController(SignInManager<ApplicationUser> signInManager, 
                                 UserManager<ApplicationUser> userManager,
                                 RoleManager<ApplicationIdentityRole> roleManager,
                                 IMapper mapper,
                                 IPatientAccountService patientAccountService
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

            var dtoModel = mapper.Map<RegisterPatientInDTO>(model);
            dtoModel.SystemRole = Role.Patient;

            var result = await patientAccountService.Register(dtoModel);

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
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}