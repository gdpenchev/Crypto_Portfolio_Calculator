using CryptoPortfolioCalculator.Models;

namespace CryptoPortfolioCalculator.Services
{
    public interface ICoinApiService
    {
        public Task<List<CoinModel>> GetCoinDataAsync();
    }
}
