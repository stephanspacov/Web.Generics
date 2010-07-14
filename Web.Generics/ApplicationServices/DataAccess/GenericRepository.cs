using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Web.Generics.ApplicationServices.DataAccess
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly IRepositoryContext context;
        public GenericRepository(IRepositoryContext context)
        {
            this.context = context;
        }

        public IQueryable<T> Select()
        {
            return this.context.Query<T>().Where(x => true);
        }

        public IQueryable<T> Select(Expression<Func<T, Boolean>> filter)
        {
            return this.context.Query<T>().Where(filter);
        }

        public void SaveOrUpdate(T obj)
        {
            this.context.SaveOrUpdate(obj);
        }

        public void Delete(T obj)
        {
            this.context.Delete(obj);
        }

        public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            this.context.Delete(filter);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public T SelectById(object id)
        {
            return this.context.SelectById<T>(id);
        }
    }
}
