using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Web.Generics.Infrastructure.DataAccess
{
    public interface IRepository<T>
    {
        IQueryable<T> Select();
        IQueryable<T> Select(Expression<Func<T, Boolean>> filter);
        void SaveOrUpdate(T obj);
        void Delete(T obj);
        T SelectById(object id);
    }
}
