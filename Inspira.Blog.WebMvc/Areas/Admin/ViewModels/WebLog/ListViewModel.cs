using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Generics.UserInterface.HtmlHelpers;

namespace Inspira.Blog.WebMvc.Areas.Admin.ViewModels.WebLog
{
    public class ListViewModel
    {
		public ListViewModel()
		{
			this.DefaultGrid = new Grid();
			this.Wrapper = new Wrapper();
			Wrapper.OutroGrid = new Grid();
		}

		public Grid DefaultGrid { get; set; }
		public Wrapper Wrapper { get; set; }
    }

	public class Wrapper
	{
		public Grid OutroGrid { get; set; }
	}
}