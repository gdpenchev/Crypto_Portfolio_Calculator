using Newtonsoft.Json;

namespace CryptoPortfolioCalculator.Models
{
    public class CoinResultModel
    {
        [JsonProperty(PropertyName = "data")]
        public List<CoinModel> Data { get; set; }
    }
}
