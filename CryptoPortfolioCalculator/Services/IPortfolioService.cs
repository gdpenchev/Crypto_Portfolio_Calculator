using CryptoPortfolioCalculator.Models;

namespace CryptoPortfolioCalculator.Services
{
    public interface IPortfolioService
    {
        Task<List<PortfolioCoinInfo>> GetCoinInputInfoAsync(IFormFile file);

        Task<List<PortfolioCoinInfo>> GetUpdatedCoinPriceInfoAsync(List<PortfolioCoinInfo> serializedPortfolio);
    }
}
