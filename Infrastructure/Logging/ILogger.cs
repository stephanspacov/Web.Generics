using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.Infrastructure.Logging
{
    public interface ILogger
    {
        void Debug(String logMessage, params object[] parameters);
        void Error(String logMessage, params object[] parameters);
        void Info(String logMessage, params object[] parameters);
        void Warn(String logMessage, params object[] parameters);
    }
}
