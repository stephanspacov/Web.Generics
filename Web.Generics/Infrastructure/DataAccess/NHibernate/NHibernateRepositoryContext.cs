using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Web.Generics.ApplicationServices.DataAccess;

namespace Web.Generics.Infrastructure.DataAccess.NHibernate
{
    public class NHibernateRepositoryContext : IRepositoryContext
    {
        private readonly ISession session;

        public NHibernateRepositoryContext(ISession session)
        {
            this.session = session;
        }

        public void SaveChanges()
        {
            session.Flush();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            var query = this.session.Linq<T>();
            return (IQueryable<T>)query;
        }

        public void SaveOrUpdate<T>(T obj) where T : class
        {
            this.session.SaveOrUpdate(obj);
        }

        public void Delete(Object obj)
        {
            this.session.Delete(obj);
        }

        public T SelectById<T>(object id)
        {
            return this.session.Get<T>(id);
        }
    }
}