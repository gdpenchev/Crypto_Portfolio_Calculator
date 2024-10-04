using CryptoPortfolioCalculator.CustomErrors;
using CryptoPortfolioCalculator.Models;
using CryptoPortfolioCalculator.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CryptoPortfolioCalculator.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        private readonly ICustomLoggerService _loggerService;

        public PortfolioController(IPortfolioService portfolioService, ICustomLoggerService loggerService)
        {
            _portfolioService = portfolioService;
            _loggerService = loggerService;
        }

        public IActionResult Portfolio()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string logFolder)
        {
            if (file is null || file.Length == 0)
            {
                return View("Error", "Please upload a valid file.");
            }

            try
            {
                SetLogFolder(logFolder);

                var portfolioCoins = await _portfolioService.GetCoinInputInfoAsync(file);

                if (portfolioCoins is null || portfolioCoins.Count == 0)
                {
                    _loggerService.ErrorLog("Portfolio with coins is empty.");
                    return RedirectToAction("Error", "Home");
                }
                var sesvalue = JsonConvert.SerializeObject(portfolioCoins);

                _loggerService.InfoLog("Setting up portofio session Id and portfolio coins.");
                HttpContext.Session.SetString($"porfolio_{HttpContext.Session.Id}", JsonConvert.SerializeObject(portfolioCoins));

                return View("Portfolio", portfolioCoins);
            }
            catch(LoggerException loggerEx)
            {
                return RedirectToAction("LoggerError", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetUpdatedPortfolio()
        {
            _loggerService.InfoLog("Start updating portfolio with latest info");
            var portfolio = HttpContext.Session.GetString($"porfolio_{HttpContext.Session.Id}");

            if (portfolio is null)
            {
                return RedirectToAction("Error", "Home");
            }

            try
            {
                var serializedPortfolio = JsonConvert.DeserializeObject<List<PortfolioCoinInfoModel>>(portfolio);

                _loggerService.InfoLog("Getting latest coin information from API.");
                var updatedPortfolio = await _portfolioService.GetUpdatedCoinPriceInfoAsync(serializedPortfolio);

                if (updatedPortfolio is null || updatedPortfolio.Count == 0)
                {
                    _loggerService.ErrorLog("Portfolio with coins is empty during update of the portfolio.");
                    return RedirectToAction("Error", "Home");
                }

                return Json(updatedPortfolio);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        private void SetLogFolder(string logFolder)
        {
            if (HttpContext.Session.GetString($"LogFolder_{HttpContext.Session.Id}") is null)
            {
                if (!Directory.Exists(logFolder))
                {
                    Directory.CreateDirectory(logFolder);

                }
                HttpContext.Session.SetString($"LogFolder_{HttpContext.Session.Id}", logFolder);
                _loggerService.InfoLog("Log folder set by the user.");
            }
        }
    }
}
