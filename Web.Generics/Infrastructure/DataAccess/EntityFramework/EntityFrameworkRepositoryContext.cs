using System;
using System.Data.Objects;
using System.Linq;
using Web.Generics.ApplicationServices.DataAccess;

namespace Web.Generics.Infrastructure.DataAccess.EntityFramework
{
    public class EntityFrameworkRepositoryContext : IRepositoryContext
    {
        private readonly ObjectContext objectContext;

        public EntityFrameworkRepositoryContext(ObjectContext context)
        {
            this.objectContext = context;
        }

        public void SaveChanges()
        {
            objectContext.SaveChanges();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            var query = this.objectContext.CreateObjectSet<T>();
            return (IQueryable<T>)query;
        }

        public void SaveOrUpdate<T>(T obj) where T : class
        {
            var objectSet = (ObjectSet<T>)this.Query<T>();
            objectSet.AddObject(obj);
        }

        public void Delete(object obj)
        {
            this.objectContext.DeleteObject(obj);
        }

        public T SelectById<T>(Object id)
        {
			throw new NotImplementedException();
        }
    }
}