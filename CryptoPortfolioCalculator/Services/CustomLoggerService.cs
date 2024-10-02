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
            var logFolder = _httpContextAccessor.HttpContext?.Session.GetString("LogFolder");

            if (string.IsNullOrEmpty(logFolder))
            {
                throw new InvalidOperationException("Log folder not set in session.");
            }

            WriteMessageInLog(logFolder, nameof(ErrorLog), message);
        }

        public void InfoLog(string message)
        {
            var logFolder = _httpContextAccessor.HttpContext?.Session.GetString("LogFolder");

            if (string.IsNullOrEmpty(logFolder))
            {
                throw new InvalidOperationException("Log folder not set in session.");
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
