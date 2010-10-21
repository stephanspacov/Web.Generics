using System;
using System.Collections.Generic;
using Web.Generics.HtmlHelpers;
using System.Linq.Expressions;
using NHibernate;

namespace Web.Generics
{
	public interface IGenericRepository<T> {
		Int32 Insert(T obj);
		Int32 Update(T obj);
		Int32 Delete(T obj);
		IList<T> Select();
		IList<T> Select(Expression<Func<T, Boolean>> expr);
		//[Obsolete("Usar Select com lambda expressions")]
        IList<T> Select(IWebGrid filter);
		T SelectById(Object id);
        Int32 Count();
        Int32 Count(IWebGrid filter);
        System.Collections.IList SelectByType(Type relatedEntityType);
    }
}