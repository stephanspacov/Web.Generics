using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.Infrastructure.DataAccess
{
	public static class LinqExtensions
	{
		public static IQueryable<T> GetPage<T>(this IQueryable<T> queryable, Int32 pageSize, Int32 pageIndex)
		{
			return queryable.Skip(pageIndex * pageSize).Take(pageSize);
		}
	}
}
