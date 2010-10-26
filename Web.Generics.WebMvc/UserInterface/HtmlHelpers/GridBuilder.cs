/*
Copyright 2010 Inspira Tecnologia.
All Rights Reserved.

Contact: Thiago Alves <thiago.alves@inspira.com.br>

This file is part of Web.Generics

Web.Generics is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Web.Generics is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with Web.Generics.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
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
			grid.PagingInfo.PagingEnabled = true;
			grid.SortingInfo.SortingEnabled = true;

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
			if (grid.SortingInfo.SortProperty != null)
			{
				var mySortExpression = grid.SortingInfo.GetSortExpression<T>();
				if (grid.SortingInfo.Order == SortOrder.Ascending)
				{
					// query = query.OrderBy(mySortExpression);
					grid.SortingInfo.Order = SortOrder.Descending;
				}
				else
				{
					// query = query.OrderByDescending(mySortExpression);
					grid.SortingInfo.Order = SortOrder.Ascending;
				}
			}

			// paging
			if (grid.PagingInfo.PagingEnabled)
			{
				query = query.Skip((grid.PagingInfo.PageIndex - 1) * grid.PagingInfo.PageSize).Take(grid.PagingInfo.PageSize);
			}

			return query;
		}
	}
}
