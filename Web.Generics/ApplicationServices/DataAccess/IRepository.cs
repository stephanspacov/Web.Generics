using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Web.Generics.ApplicationServices.DataAccess
{
    public interface IRepository<T> : IQueryable<T>
    {
        IQueryable<T> Query();
        void SaveOrUpdate(T obj);
        void Delete(T obj);
        T SelectById(object id);
		void SaveChanges();
		IList<T> SelectWithPagingAndSorting(Expression<Func<T, bool>> expression, int? pageSize, int? pageIndex, Expression<Func<T, object>> sortProperty, SortOrder? sortOrder, out int totalItemCount);
	}
}
