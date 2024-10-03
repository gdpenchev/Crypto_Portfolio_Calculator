using Newtonsoft.Json;

namespace CryptoPortfolioCalculator.Models
{
    public class CoinResult
    {
        [JsonProperty(PropertyName = "data")]
        public List<Coin> Data { get; set; }
    }
}
