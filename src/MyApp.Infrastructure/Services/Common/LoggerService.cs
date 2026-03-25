
using Microsoft.Extensions.Logging;
using MyApp.Application.Interfaces.Common;
using NLog;

namespace MyApp.Infrastructure.Services.Common
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;
        private readonly string _logFilePath;
        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");
        }

        public void Log(string message)
        {
            var log = $"[{DateTime.Now}] INFO: {message}\n";
            File.AppendAllText(_logFilePath, log);
        }

        public void LogError(string errorMessage)
        {
            _logger.LogError(errorMessage);
        }

        public void LogError(string errorMessage, params object[] args)
        {
            _logger.LogError(errorMessage, args);
        }

        public void LogException(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        public void LogInfo(string infoMessage)
        {
            _logger.LogInformation(infoMessage);
        }

        public void LogInfo(string infoMessage, params object[] args)
        {
            _logger.LogInformation(infoMessage, args);
        }
    }

}
