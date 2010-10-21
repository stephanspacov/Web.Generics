using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.UserInterface.Models;

namespace Web.Generics.UserInterface
{
	public class GenericViewModel
	{
		public GenericViewModel()
		{
			this.DefaultGrid = new Grid();
		}
		public Grid DefaultGrid { get; set; }
		public Object Instance { get; set; }
	}
}
