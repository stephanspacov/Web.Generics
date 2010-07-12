using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Generics.Tests.Logging;
using System.IO;
using Web.Generics.Tests.Repositories;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.Infrastructure.Logging;

namespace Web.Generics.Tests
{
    [TestClass]
    public class Infrastructure_Logging_Tests
    {
        [TestMethod]
        public void Log()
        {
            var stringBuilder = new StringBuilder();
            var sw = new StringWriter(stringBuilder);

            var logger = new MockLogger(sw);
            logger.Warn("{0}", 1);
            logger.Debug("{0}", 2);
            logger.Error("{0}", 3);
            logger.Info("{0}", 4);

            Assert.AreEqual(
                "Warn: 1" + Environment.NewLine +
                "Debug: 2" + Environment.NewLine +
                "Error: 3" + Environment.NewLine +
                "Info: 4" + Environment.NewLine,
                sw.ToString());
        }

        [TestMethod]
        public void Log_With_Aspect()
        {
            var stringBuilder = new StringBuilder();
            var sw = new StringWriter(stringBuilder);

            var attr = new LoggableAttribute();
            var logger = new MockLogger(sw);
            logger.Warn("{0}", 1);
            logger.Debug("{0}", 2);
            logger.Error("{0}", 3);
            logger.Info("{0}", 4);

            Assert.AreEqual(
                "Warn: 1" + Environment.NewLine +
                "Debug: 2" + Environment.NewLine +
                "Error: 3" + Environment.NewLine +
                "Info: 4" + Environment.NewLine,
                sw.ToString());
        }
    }
}
