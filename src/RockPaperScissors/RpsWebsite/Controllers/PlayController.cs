using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RpsWebsite.Entities;
using RpsWebsite.Services;
using RpsWebsite.ViewModels;
using System;

namespace RpsWebsite.Controllers
{
    [Authorize]
    public class PlayController : Controller
    {
        private UserManager<User> _userManager;
        private IRpsServerClient _client;

        public PlayController(UserManager<User> userManager, IRpsServerClient client)
        {
            _userManager = userManager;
            _client = client;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).GetAwaiter().GetResult();
            if (user == null || user.UserId == Guid.Empty)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            ViewBag.PlayerId = user.UserId.ToString();
            ViewBag.PlayerWins = user.Wins;
            ViewBag.PlayerLosses = user.Losses;
            ViewBag.PlayerDraws = user.Draws;
            ViewBag.Endpoint = _client.Endpoint;

            return View();
        }

        [HttpPost("play/result/{security}")]
        public IActionResult Result(int security)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).GetAwaiter().GetResult();
            if (user == null || user.UserId == Guid.Empty)
                return new BadRequestResult();

            // lolol I'll make this better if we keep working on this
            switch (security)
            {
                case 329847:
                    ++user.Wins;
                    break;

                case 87321:
                    ++user.Losses;
                    break;

                case 6719422:
                    ++user.Draws;
                    break;

                default:
                    return new StatusCodeResult(StatusCodes.Status402PaymentRequired); // pay me money and I'll let you cheat
            }

            _userManager.UpdateAsync(user).GetAwaiter().GetResult();

            return new OkResult();
        }
    }
}
