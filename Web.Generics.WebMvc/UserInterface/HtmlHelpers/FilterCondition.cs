using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.WebMvc.UserInterface.HtmlHelpers
{
	public class FilterCondition
	{
		public String PropertyName { get; set; }
		public FilterComparerType Comparer { get; set; }
		public Object Value { get; set; }
	}
}