namespace CryptoPortfolioCalculator.Models
{
    public class PortfolioCoinInfo
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public decimal InitialBuyPrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal InitialValue => Math.Round(Amount * InitialBuyPrice, 2, MidpointRounding.ToEven);

        public decimal CurrentValue => Math.Round(Amount * CurrentPrice, 2, MidpointRounding.ToEven);

        public decimal PercentageChange => Math.Round(((CurrentPrice - InitialBuyPrice) / InitialBuyPrice) * 100, 2, MidpointRounding.ToEven);
    }
}
