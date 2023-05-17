using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Application.Interfaces
{
    public interface ILoggerService
    {
        void LogInfo(string message, params object?[] args);
        void LogWarning(string message);
        void LogDebug(string message);
        void LogError(Exception exception, string message);
    }
}
