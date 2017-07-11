using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RpsWebsite.Services;
using RpsWebsite.ViewModels;

namespace RpsWebsite.Controllers
{
    [Authorize]
    public class PlayController : Controller
    {
        private IRpsServerClient _client;

        public PlayController(IRpsServerClient client)
        {
            _client = client;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Endpoint = _client.Endpoint;

            return View();
        }

        [HttpGet]
        public IActionResult InGame(GameViewModel game)
        {
            ViewBag.GameId = game.GameId;

            return View();
        }
    }
}
