using DigitalLibrary.Extensions;
using DigitalLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DigitalLibrary.Controllers
{
    public class HomeController : MainController
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppSettings _appSettings;
        public HomeController(ILogger<HomeController> logger,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User appUser)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(appUser.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(appUser.Email, appUser.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return ErrorMessageRedirect("Dados incorretos!", "Login", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }


    }
}
