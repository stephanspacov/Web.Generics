using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.UserInterface.HtmlHelpers
{
    public class GridRow
    {
		public GridRow()
		{
			this.Cells = new List<GridCell>();
		}

		public GridRow(params Object[] cellValues) : this()
		{
			foreach (var cellValue in cellValues)
			{
				this.Cells.Add(new GridCell { Value = cellValue });
			}
		}

		public IList<GridCell> Cells { get; set; }

		public Object KeyValue { get; set; }
		public String KeyName { get; set; }
	}
}
