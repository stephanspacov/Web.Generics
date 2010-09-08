using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.UserInterface.HtmlHelpers;
using System.Linq.Expressions;

namespace Web.Generics.DomainServices
{
    public class SortingInfo
    {
        public Boolean SortingEnabled { get; set; }
		public String SortProperty { get; set; }
		public SortOrder PreviousSortOrder { get; set; }
		public String PreviousSortProperty { get; set; }
		public Expression<Func<T, Object>> GetSortExpression<T>()
		{
			if (SortProperty == null) return null;

			var param = Expression.Parameter(typeof(T), "p");
			return Expression.Lambda<Func<T, Object>>(Expression.Convert(Expression.Property(param, SortProperty), typeof(Object)), param);
		}
        public SortOrder SortOrder { get; set; }
    }
}
