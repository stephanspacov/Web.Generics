using System;
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
