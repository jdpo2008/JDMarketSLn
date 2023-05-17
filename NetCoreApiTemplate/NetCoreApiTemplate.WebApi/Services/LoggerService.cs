using NetCoreApiTemplate.Application.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.WebApi.Services
{
    public class LoggerService : ILoggerService
    {
        private static NLog.ILogger logger = LogManager.Setup().LoadConfigurationFromFile().GetCurrentClassLogger();
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogError(Exception exception, string message)
        {
            logger.Error(exception, message);
        }
        public void LogInfo(string message, params object?[] args)
        {
            logger.Info(message, args);
        }
        public void LogWarning(string message)
        {
            logger.Warn(message);
        }
    }
}
