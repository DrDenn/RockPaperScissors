using Microsoft.AspNetCore.Mvc;

namespace RpsWebsite.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
