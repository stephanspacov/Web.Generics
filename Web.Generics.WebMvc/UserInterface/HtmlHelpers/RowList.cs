using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.UserInterface.HtmlHelpers
{
	public class RowList
	{
		public Boolean AllowPaging { get; set; }
		public Boolean AllowSorting { get; set; }
		public String SortProperty { get; set; }
		public SortOrder SortOrder { get; set; }
		public Int32 PageSize { get; set; }
		public Int32 PageIndex { get; set; }
	}
}
