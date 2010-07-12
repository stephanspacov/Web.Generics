using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using System.Data.Objects;

namespace Web.Generics.Infrastructure.DataAccess
{
    public interface IRepositoryContext
    {
        void SaveChanges();
        void SaveOrUpdate<T>(T obj) where T : class;
        void Delete(Object obj);
        IQueryable<T> Query<T>() where T : class;
        T SelectById<T>(object id);
    }
}
