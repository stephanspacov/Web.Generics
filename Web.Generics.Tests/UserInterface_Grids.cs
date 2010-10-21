using System;
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
