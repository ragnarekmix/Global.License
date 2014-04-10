using System;

namespace Global.LicenseManager.Common.Log
{
    public interface ILogger
    {
        void Error(string message);

        void Error(string message, Exception exception);

        void Info(string message);

        void InfoFormat(string message, params object[] parameters);

        void Debug(string message);

        void DebugFormat(string message, params object[] parameters);
    }
}