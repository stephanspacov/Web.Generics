using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.WebMvc.UserInterface.HtmlHelpers;
using System.Collections;
using Web.Generics.UserInterface.Components;

namespace Web.Generics.UserInterface.Models
{
    public class Grid : RowList
    {
		public Grid() {
			this.Columns = new List<GridColumn>();
			this.Rows = new List<GridRow>();
		}

        public Boolean AutoGenerateColumns { get; set; }
        public IList<GridColumn> Columns { get; set; }
        public IList<GridRow> Rows { get; set; }

        public IEnumerable DataSource { get; set; }

        public void DataBind()
        {
            if (AutoGenerateColumns == true)
            {
                var builder = new GridBuilder(this);
                builder.Populate(this.DataSource);
            }
            else
            {
                foreach (Object o in this.DataSource)
                {
                    var gridRow = new GridRow();
                    foreach (GridColumn col in this.Columns)
                    {
                        if (o != null) {
                            var propertyInfo = o.GetType().GetProperty(col.PropertyName);
                            Object propertyValue = propertyInfo.GetValue(o, null);
                            String cellText;
                            if (propertyValue == null) {
                                cellText = "";
                            } else {
                                cellText = propertyValue.ToString();
                            }
                            gridRow.Cells.Add(new GridCell(cellText));
                            
                        }
                    }
                    this.Rows.Add(gridRow);
                }
            }
        }
    }
}
