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
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Generics.UserInterface.Models;

namespace Web.Generics.Tests.UserInterface
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void Create_gridcolumns_0_items()
        {
            var columns = GridColumn.Create();
            Assert.AreEqual(0, columns.Count);
        }

        [TestMethod]
        public void Create_gridcolumns_2_items()
        {
            var columns = GridColumn.Create("Property", "Header text");
            Assert.AreEqual("Property", columns[0].PropertyName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_gridcolumns_3_items()
        {
            var columns = GridColumn.Create("Property", "Header text", "3");
        }

        [TestMethod]
        public void Create_gridcolumns_4_items()
        {
            var columns = GridColumn.Create("Property", "Header text", "Property 2", "Header text 2");
            Assert.AreEqual("Property", columns[0].PropertyName);
            Assert.AreEqual("Property 2", columns[1].PropertyName);
        }
    }
}
