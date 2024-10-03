using CryptoPortfolioCalculator.Models;

namespace CryptoPortfolioCalculator.Services
{
    public interface IPortfolioService
    {
        Task<List<PortfolioCoinInfoModel>> GetCoinInputInfoAsync(IFormFile file);

        Task<List<PortfolioCoinInfoModel>> GetUpdatedCoinPriceInfoAsync(List<PortfolioCoinInfoModel> serializedPortfolio);
    }
}
