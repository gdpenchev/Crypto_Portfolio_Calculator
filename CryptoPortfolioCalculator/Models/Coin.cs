using Newtonsoft.Json;

namespace CryptoPortfolioCalculator.Models
{
    public class Coin
    {
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }

        [JsonProperty(PropertyName = "price_usd")]
        public decimal Price { get; set; }
    }
}
