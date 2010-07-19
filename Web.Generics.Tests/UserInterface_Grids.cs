using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;

namespace Web.Generics.Tests
{
	[TestClass]
	public class UserInterface_Grids
	{
		GenericRepository<Post> Posts;

		[TestInitialize]
		public void Initialize()
		{
			//new 
			//Posts = new GenericRepository<Post>();
		}

		[TestMethod]
		public void Show_Items_In_The_Same_Order_From_DB()
		{
		}
	}
}
