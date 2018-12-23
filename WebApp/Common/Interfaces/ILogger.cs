using Common.Models;
using System;
using System.Collections.Generic;

namespace Common.Interfaces
{
    public interface ILogger<T> : ILogger{}

    public interface ILogger
    {
        void LogError(string message, Exception ex, DateTime dateTime);
        void LogError(string message, Exception ex);
        void LogInfo(string message, DateTime dateTime, object args);
        void LogInfo(string message, object args);
        void LogInfo(string message, DateTime dateTime);
        void LogInfo(string message);
        void RemoveInfoLog(string objetId);
        void RemoveErrorLog(string objectId);
        void ClearAllInfoLogs();
        void ClearAllErrorLogs();
        IEnumerable<InfoLogModel> InfoLogs();
        IEnumerable<ErrorLogModel> ErrorLogs();
        ErrorLogModel GetErrorLog(string objectId);
        InfoLogModel GetInfoLog(string objectId);
    }
}
