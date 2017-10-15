using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RpsWebsite.Entities;
using System;

namespace RpsWebsite.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).GetAwaiter().GetResult();
                if (user != null)
                {
                    ViewBag.PlayerWins = user.Wins;
                    ViewBag.PlayerLosses = user.Losses;
                    ViewBag.PlayerDraws = user.Draws;
                }
            }

            return View();
        }
    }
}
