using Microsoft.AspNetCore.Mvc;

namespace CryptoPortfolioCalculator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View("Error");
        }

        public IActionResult LoggerError()
        {
            return View("LoggerError");
        }
    }
}
