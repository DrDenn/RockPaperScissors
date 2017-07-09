using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RpsWebsite.Entities;
using RpsWebsite.ViewModels;
using System.Threading.Tasks;

namespace RpsWebsite.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        /// <summary>
        /// Creates an instance of the Account Manger.
        /// This class uses the ASP.NET Identity EntityFramework libraries to control login and registration.
        /// </summary>
        /// <param name="userManager">Entity Framework Identity user manager</param>
        /// <param name="signInManager">Entity Framework Identity sign in manager</param>
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Login to the website.
        /// GET action to view the login page.
        /// </summary>
        /// <returns>The login view.</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Login to the website.
        /// POST action to attempt a login.
        /// </summary>
        /// <param name="model">The login view model.</param>
        /// <returns>Result of the login attempt.</returns>
        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: lockout on failure is vulnerable
                var loginResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (loginResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "UserName or Password is incorrect!");
            return View();
        }

        /// <summary>
        /// Register a new user to the website.
        /// GET action to view the registration page.
        /// </summary>
        /// <returns>The register view.</returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register a new user to the website.
        /// POST action to attempt registration of a new user.
        /// </summary>
        /// <param name="userModel">The register view model.</param>
        /// <returns>Result of the register attempt.</returns>
        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User(model.UserName);

                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in createResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View();
        }

        /// <summary>
        /// Log out the current user.
        /// </summary>
        /// <returns>Redirect to home page after logout.</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
