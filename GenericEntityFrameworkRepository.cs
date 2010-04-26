using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Web.Generics
{
    public class GenericEntityFrameworkRepository<T> : IGenericRepository<T> where T : class
    {
        ObjectContext context;
        IObjectSet<T> objectSet;

        public GenericEntityFrameworkRepository(ObjectContext context)
        {
            this.context = context;
        }

        private IObjectSet<T> ObjectSet
        {
            get
            {
                if (objectSet == null)
                {
                    objectSet = this.context.CreateObjectSet<T>();
                }
                return objectSet;
            }
        }

        #region IGenericRepository<T> Members

        public IQueryable<T> GetQuery()
        {
            return ObjectSet;
        }

        public int Insert(T obj)
        {
            this.ObjectSet.AddObject(obj);

            return 0;
        }

        public int Update(T obj)
        {
            this.ObjectSet.Attach(obj);

            return 0;
        }

        public int Delete(T obj)
        {
            this.ObjectSet.DeleteObject(obj);

            return 0;
        }

        public IList<T> Select()
        {
            return this.ObjectSet.ToList<T>();
        }

        public T SelectById(int id)
        {
            return this.ObjectSet.SingleOrDefault<T>();
        }

        public IList<T> Select(FilterParameters filter)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Count(FilterParameters filter)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
