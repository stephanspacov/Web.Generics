using System;
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
