using CryptoPortfolioCalculator.Models;
using System.Globalization;

namespace CryptoPortfolioCalculator.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly ICoinApiService _coinApiService;
        private readonly ICustomLoggerService _LoggerService;

        public PortfolioService(ICoinApiService coinApiService, ICustomLoggerService customLoggerService)
        {
            _coinApiService = coinApiService;
            _LoggerService = customLoggerService;
        }
        public async Task<List<PortfolioCoinInfoModel>> GetCoinInputInfoAsync(IFormFile file)
        {
            var portfolioCoins = new List<PortfolioCoinInfoModel>();
            _LoggerService.InfoLog("Reading portfolio information from uploaded file.");
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var columns = line?.Split('|');

                    if (columns?.Length == 3)
                    {
                        var amount = decimal.Parse(columns[0], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                        var coinName = columns[1];
                        var initialBuyPrice = decimal.Parse(columns[2], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

                        portfolioCoins.Add(new PortfolioCoinInfoModel
                        {
                            Amount = amount,
                            Name = coinName,
                            InitialBuyPrice = initialBuyPrice
                        });
                    }
                }
                _LoggerService.InfoLog("Finished reading portfolio information from uploaded file.");
            }
            return await GetUpdatedCoinPriceInfoAsync(portfolioCoins);
        }

        public async Task<List<PortfolioCoinInfoModel>> GetUpdatedCoinPriceInfoAsync(List<PortfolioCoinInfoModel> portfolioCoins)
        {
            var coins = await _coinApiService.GetCoinDataAsync();

            if (coins is null || coins.Count == 0)
            {
                return new List<PortfolioCoinInfoModel>();
            }
            foreach (var item in portfolioCoins)
            {
                var coin = coins.FirstOrDefault(c => c.Symbol.Equals(item.Name, StringComparison.OrdinalIgnoreCase));

                if (coin is not null)
                {
                    item.CurrentPrice = coin.Price;
                }

            }
            return portfolioCoins;
        }
    }
}
