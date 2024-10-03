using CryptoPortfolioCalculator.CustomErrors;

namespace CryptoPortfolioCalculator.Services
{
    public class CustomLoggerService : ICustomLoggerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomLoggerService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void ErrorLog(string message)
        {
            var sessionId = _httpContextAccessor.HttpContext?.Session.Id;
            var logFolder = _httpContextAccessor.HttpContext?.Session.GetString($"LogFolder_{sessionId}");

            if (string.IsNullOrEmpty(logFolder))
            {
                throw new LoggerException("Log folder not set in session.");
            }

            WriteMessageInLog(logFolder, nameof(ErrorLog), message);
        }

        public void InfoLog(string message)
        {
            var sessionId = _httpContextAccessor.HttpContext?.Session.Id;
            var logFolder = _httpContextAccessor.HttpContext?.Session.GetString($"LogFolder_{sessionId}");
            
            if (string.IsNullOrEmpty(logFolder))
            {
                throw new LoggerException("Log folder not set in session.");
            }

            WriteMessageInLog(logFolder, nameof(InfoLog), message);
            
        }

        private void WriteMessageInLog(string logFolder, string messageOfType, string message)
        {
            var fullLogFilePath = Path.Combine(logFolder, $"Portfolio_Log_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.txt");

            using (var writer = new StreamWriter(fullLogFilePath, true))
            {
                writer.WriteLine($"[{DateTime.Now}]: {messageOfType}: {message}");
            }
        }
    }
}
