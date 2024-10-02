using CryptoPortfolioCalculator.Models;

namespace CryptoPortfolioCalculator.Services
{
    public interface ICoinApiService
    {
        public Task<List<Coin>> GetCoinDataAsync();
    }
}
