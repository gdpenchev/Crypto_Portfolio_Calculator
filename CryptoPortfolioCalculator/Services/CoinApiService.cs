using CryptoPortfolioCalculator.Models;
using Newtonsoft.Json;

namespace CryptoPortfolioCalculator.Services
{
    
    public class CoinApiService : ICoinApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ICustomLoggerService _loggerService;
        private const string _apiUrl = "https://api.coinlore.net/api/tickers/";

        public CoinApiService(HttpClient httpClient, ICustomLoggerService customLoggerService)
        {
            _httpClient = httpClient;
            _loggerService = customLoggerService;
        }
        public async Task<List<CoinModel>> GetCoinDataAsync()
        {
            try
            {
                _loggerService.InfoLog("Beggin fetching data from API.");
                var response = await _httpClient.GetAsync(_apiUrl);
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CoinResultModel>(jsonData);

                    if (result?.Data != null)
                    {
                        _loggerService.InfoLog("Coin data is collected from API.");
                        return result.Data;
                    }
                    _loggerService.ErrorLog("Coin data returned from API is empty.");
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                _loggerService.ErrorLog($"Problem with API returned exception: {httpRequestException.Message}");
                throw new HttpRequestException("Problem with API request.");
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLog($"Other problem with API returned exception: {ex.Message}");
                throw new HttpRequestException("Other problem with API request.");
            }
            return new List<CoinModel>();
        }
    }
}
