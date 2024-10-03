namespace CryptoPortfolioCalculator.Models
{
    public class PortfolioCoinInfoModel
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public decimal InitialBuyPrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal InitialValue => Amount * InitialBuyPrice;

        public decimal CurrentValue => Amount * CurrentPrice;
        public decimal PercentageChange => Math.Round((CurrentPrice - InitialBuyPrice) / InitialBuyPrice * 100, 2);
    }
}
