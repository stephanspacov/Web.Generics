using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.WebMvc.UserInterface.HtmlHelpers
{
	public class Filter
	{
		public FilterComparerType ComparerType { get; set; }
		public Filter Conditions { get; set; }
	}
}
