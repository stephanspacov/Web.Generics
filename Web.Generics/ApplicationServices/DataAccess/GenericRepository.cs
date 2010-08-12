using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Web.Generics.ApplicationServices.DataAccess
{
    public class GenericRepository<T> : IRepository<T>, IQueryable<T> where T : class
    {
        protected readonly IRepositoryContext context;
        public GenericRepository(IRepositoryContext context)
        {
            this.context = context;
        }

        public IQueryable<T> Query()
        {
            return this.context.Query<T>();
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

        public IEnumerator<T> GetEnumerator()
        {
            return Query().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Query().GetEnumerator();
        }

        public Type ElementType
        {
            get { return Query().ElementType; }
        }

        public Expression Expression
        {
            get { return Query().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return Query().Provider; }
        }
    }
}
