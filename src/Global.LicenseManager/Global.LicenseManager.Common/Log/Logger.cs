using System;
using log4net;

namespace Global.LicenseManager.Common.Log
{
    public class Logger : ILogger
    {
        ILog logger;

        public Logger()
        {
            logger = LogManager.GetLogger(typeof(Logger));
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            logger.Error(message, exception);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void InfoFormat(string message, params object[] parameters)
        {
            logger.InfoFormat(message, parameters);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void DebugFormat(string message, params object[] parameters)
        {
            logger.DebugFormat(message, parameters);
        }
    }
}