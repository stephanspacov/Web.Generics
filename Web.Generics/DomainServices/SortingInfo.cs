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
using System.Linq.Expressions;
using Web.Generics.ApplicationServices.DataAccess;

namespace Web.Generics.DomainServices
{
    public class SortingInfo
    {
        public Boolean SortingEnabled { get; set; }

		public SortOrder? Order { get; set; }
		public String SortProperty { get; set; }

		public SortOrder? PreviousOrder { get; set; }
		public String PreviousSortProperty { get; set; }

		public Expression<Func<T, Object>> GetSortExpression<T>()
		{
			var sortProperty = SortProperty;
			if (SortProperty == null)
			{
				sortProperty = PreviousSortProperty;
			}
			if (sortProperty == null) return null;		

			Expression<Func<T, Object>> sortExpression;

			var param = Expression.Parameter(typeof(T), "p");

			var propertyStack = sortProperty.Split('.');
			MemberExpression expressionProperty = Expression.Property(param, propertyStack[0]);

			for (var i = 1; i < propertyStack.Length; i++)
			{
				expressionProperty = Expression.Property(expressionProperty, propertyStack[i]);
			}

			sortExpression = Expression.Lambda<Func<T, Object>>(Expression.Convert(expressionProperty, typeof(Object)), param);
			return sortExpression;
		}

		public SortOrder GetSortOrder()
		{
			SortOrder sortOrder;

			if (this.SortProperty == null)
			{
				if (this.PreviousOrder == null)
				{
					sortOrder = SortOrder.Ascending;
				}
				else
				{
					sortOrder = this.PreviousOrder.Value;
				}
			}
			else if (this.SortProperty != null && this.SortProperty == this.PreviousSortProperty && this.PreviousOrder == SortOrder.Ascending)
			{
				sortOrder = SortOrder.Descending;
			}
			else
			{
				sortOrder = SortOrder.Ascending;
			}

            if (this.SortProperty != null) this.PreviousSortProperty = this.SortProperty;

			return sortOrder;
		}

    }
}
