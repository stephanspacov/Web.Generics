using System;
using System.Collections.Generic;

namespace Web.Generics
{
	public interface IGenericRepository<T> {
		Int32 Insert(T obj);
		Int32 Update(T obj);
		Int32 Delete(T obj);
		IList<T> Select();
        IList<T> Select(FilterParameters filter);
		T SelectById(Object id);
        Int32 Count();
        Int32 Count(FilterParameters filter);
        System.Collections.IList SelectByType(Type relatedEntityType);
    }
}