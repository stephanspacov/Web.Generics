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
		ILog logger = log4net.LogManager.GetLogger("Web.Generics");
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

		public static ILogger GetLogger<T>()
		{
			return new Log4NetLogger();
		}
	}
}
