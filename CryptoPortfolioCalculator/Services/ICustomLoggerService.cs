namespace CryptoPortfolioCalculator.Services
{
    public interface ICustomLoggerService
    {
        void InfoLog(string message);

        void ErrorLog(string message);
    }
}
