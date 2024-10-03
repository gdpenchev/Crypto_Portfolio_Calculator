using CryptoPortfolioCalculator.Models;
using Newtonsoft.Json;

namespace CryptoPortfolioCalculator.Services
{
    
    public class CoinApiService : ICoinApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ICustomLoggerService _customLoggerService;
        private const string _apiUrl = "https://api.coinlore.net/api/tickers/";

        public CoinApiService(HttpClient httpClient, ICustomLoggerService customLoggerService)
        {
            _httpClient = httpClient;
            _customLoggerService = customLoggerService;
        }
        public async Task<List<Coin>> GetCoinDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiUrl);
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CoinResult>(jsonData);

                    if (result?.Data != null)
                    {
                        _customLoggerService.InfoLog("Coin data is collected from API.");
                        return result.Data;
                    }
                    _customLoggerService.ErrorLog("Coin data returned from API is empty.");
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                _customLoggerService.ErrorLog($"Problem with API returned exception: {httpRequestException.Message}");
                throw new HttpRequestException("Problem with API request.");
            }
            catch (Exception ex)
            {
                _customLoggerService.ErrorLog($"Other problem with API returned exception: {ex.Message}");
                throw new HttpRequestException("Other problem with API request.");
            }
            return new List<Coin>();
        }
    }
}
