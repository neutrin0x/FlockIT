using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class Logger : WebApi.Services.ILogger
    {
        private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        private static Logger instance;

        public static Logger GetInstance()
        {
            if (Logger.instance == null)
                instance = new Logger();
            return instance;
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
