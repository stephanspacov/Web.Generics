/*
Copyright 2010 Inspira Tecnologia.
All Rights Reserved.

Contact: Thiago Alves <thiago.alves@inspira.com.br>

This file is part of Web.Generics

Web.Generics is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Web.Generics is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with Web.Generics.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
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
