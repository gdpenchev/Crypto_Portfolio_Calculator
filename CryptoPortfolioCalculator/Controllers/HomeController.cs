using Microsoft.AspNetCore.Mvc;

namespace CryptoPortfolioCalculator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(string message)
        {
            return View("Error", message);
        }
    }
}
