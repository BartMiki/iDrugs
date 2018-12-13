using System;

namespace Common.Interfaces
{
    public interface ILogger<T> : ILogger{}

    public interface ILogger
    {
        void LogError(string message, Exception ex, DateTime dateTime);
        void LogError(string message, Exception ex);
        void LogInfo(string message, DateTime dateTime);
        void LogInfo(string message);
    }
}
