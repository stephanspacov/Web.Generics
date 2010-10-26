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
