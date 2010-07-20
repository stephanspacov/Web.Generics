using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Web.Generics.ApplicationServices.DataAccess;
using System.Linq.Expressions;

namespace Web.Generics.UserInterface.HtmlHelpers
{
	public class GridBuilder
	{
		private readonly Grid grid;
		public GridBuilder(Grid grid)
		{
			this.grid = grid;
		}

		public void Populate(IEnumerable objectList)
		{
			grid.AllowPaging = true;
			grid.AllowSorting = true;

			var count = 0;

			foreach (var obj in objectList)
			{
				if (obj == null) throw new ArgumentNullException();
				var properties = obj.GetType().GetProperties().Where(p => !p.PropertyType.IsGenericType || p.PropertyType.GetGenericTypeDefinition() != typeof(IList<>));

				if (count == 0)
				{
					grid.Columns = properties.Select(p => new GridColumn { PropertyName = p.Name, HeaderText = p.Name }).ToList(); // TODO: pegar HeaderText via atributo DisplayName
				}

				grid.Rows.Add(
					new GridRow {
						Cells = properties.Select(p => new GridCell { Value = p.GetValue(obj, null) } ).ToList()
					}
				);

				count++;
			}
		}

		public IQueryable<T> GetDataSourceByParameters<T>(IQueryable<T> query)
		{
			// filtering

			// sorting
			if (grid.SortProperty != null)
			{
				var param = Expression.Parameter(typeof(T), "p");
				var mySortExpression = Expression.Lambda<Func<T, Object>>(Expression.Convert(Expression.Property(param, grid.SortProperty), typeof(Object)), param);

				if (grid.SortOrder == SortOrder.Ascending)
				{
					query = query.OrderBy(mySortExpression);
					grid.SortOrder = SortOrder.Descending;
				}
				else
				{
					query = query.OrderByDescending(mySortExpression);
					grid.SortOrder = SortOrder.Ascending;
				}
			}

			// paging
			if (grid.AllowPaging)
			{
				query = query.Skip((grid.PageIndex - 1) * grid.PageSize).Take(grid.PageSize);
			}

			return query;
		}
	}
}
