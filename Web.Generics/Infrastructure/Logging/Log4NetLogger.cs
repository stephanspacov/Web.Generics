using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using Web.Generics;

namespace Web.Generics.Infrastructure.Logging
{
	public class Log4NetLogger : ILogger
	{
		ILog logger;
        public Log4NetLogger() : this("LogInFile") { }
        internal Log4NetLogger(String logName)
        {
            this.logger = log4net.LogManager.GetLogger(logName);
        }

		public void Debug(String logMessage, params object[] parameters)
		{
			logger.Debug(String.Format(logMessage, parameters));
		}
		public void Info(String logMessage, params object[] parameters)
		{
			logger.Info(String.Format(logMessage, parameters));
		}
		public void Error(String logMessage, params object[] parameters)
		{
			logger.Error(String.Format(logMessage, parameters));
		}
		public void Warn(String logMessage, params object[] parameters)
		{
			logger.Warn(String.Format(logMessage, parameters));
		}
	}
}
