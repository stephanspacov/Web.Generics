using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Generics.UserInterface.HtmlHelpers;

namespace Inspira.Blog.WebMvc.Areas.Admin.ViewModels.Post
{
	public class IndexViewModel
	{
		public IndexViewModel()
		{
			this.AllPosts = new Grid();
		}

		public Grid AllPosts { get; set; }
	}
}