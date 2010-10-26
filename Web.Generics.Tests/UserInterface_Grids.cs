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
using System.Linq;
using Inspira.Blog.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.Infrastructure.DataAccess;
using Web.Generics.UserInterface.Components;
using Web.Generics.UserInterface.Models;

namespace Web.Generics.Tests
{
	[TestClass]
	public class UserInterface_Grids
	{
		GenericRepository<WebLog> WebLogs;

		[TestInitialize]
		public void Initialize()
		{
			var context = ContextFactory.GetContext();
			this.WebLogs = new GenericRepository<WebLog>(context);

			var owner = new User { Name = "user" };
			this.WebLogs.SaveOrUpdate(new WebLog { CreatedAt = DateTime.Today, Title = "Blog 1", Collaborators = new[] { owner } });
			this.WebLogs.SaveOrUpdate(new WebLog { CreatedAt = DateTime.Today, Title = "Blog 2", Collaborators = new[] { owner } });

			this.WebLogs.SaveChanges();
		}

		[TestMethod]
		public void Show_Items_In_Grid()
		{
			var dataSource = this.WebLogs.ToList();

			var grid = new Grid();

			var gridBuilder = new GridBuilder(grid);
			gridBuilder.Populate(dataSource);

			Assert.AreEqual(3, grid.Columns.Count);
			Assert.AreEqual(2, grid.Rows.Count);
			Assert.AreEqual(DateTime.Today.ToString(), grid.Rows[0].Cells[2].Text);
			Assert.AreEqual("Blog 2", grid.Rows[1].Cells[1].Text);
		}


		[TestMethod]
		public void Paging()
		{
			var dataSource = this.WebLogs.GetPage(1, 1);

			var grid = new Grid();

			var gridBuilder = new GridBuilder(grid);
			gridBuilder.Populate(dataSource);

			Assert.AreEqual(3, grid.Columns.Count);
			Assert.AreEqual(1, grid.Rows.Count);
			Assert.AreEqual(DateTime.Today.ToString(), grid.Rows[0].Cells[2].Text);
			Assert.AreEqual("Blog 2", grid.Rows[0].Cells[1].Text);
		}
	}
}
