using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.UserInterface.HtmlHelpers;
using System.Collections;

namespace Inspira.Blog.WebMvc.Areas.Admin.Controllers
{
	class GridDataBinder
	{
		internal Web.Generics.UserInterface.HtmlHelpers.Grid GetGrid(IEnumerable objectList)
		{
			return new Grid
			{
				Columns = new[] {
						new GridColumn { PropertyName = "ID", HeaderText="Code" },
						new GridColumn { PropertyName = "Username", HeaderText="User name" },
						new GridColumn { PropertyName = "Name", HeaderText="First Name" },
						new GridColumn { PropertyName = "Surname", HeaderText="Last Name" },
					},
				Rows = new[] {
						new GridRow(1, "hulk", "Hulk", "My god"),
						new GridRow(2, "pele", "Pelé", "The king"),
						new GridRow(3, "mara", "Maradona", "La mano de dios"),
					},
				AllowSorting = true
			};
		}
	}
}
