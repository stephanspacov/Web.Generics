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
using System.Text;
using Web.Generics.Infrastructure.Logging;
using System.IO;

namespace Web.Generics.Tests.Logging
{
    public class MockLogger : ILogger
    {
        StringWriter writer;
        public MockLogger(StringWriter writer)
        {
            this.writer = writer;
        }

        public void Debug(string logMessage, params object[] parameters)
        {
            this.writer.WriteLine("Debug: " + logMessage, parameters);
        }

        public void Error(string logMessage, params object[] parameters)
        {
            this.writer.WriteLine("Error: " + logMessage, parameters);
        }

        public void Info(string logMessage, params object[] parameters)
        {
            this.writer.WriteLine("Info: " + logMessage, parameters);
        }

        public void Warn(string logMessage, params object[] parameters)
        {
            this.writer.WriteLine("Warn: " + logMessage, parameters);
        }
    }
}
