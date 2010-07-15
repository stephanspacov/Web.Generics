using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Web.Generics.ApplicationServices.DataAccess;

namespace Web.Generics.DomainServices
{
	public class GenericService<T>
	{
        private readonly IRepository<T> genericRepository;
        public GenericService(IRepository<T> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

		virtual public void SaveOrUpdate(T obj)
		{
			this.genericRepository.SaveOrUpdate(obj);
		}

        virtual public void Delete(T obj)
		{
			this.genericRepository.Delete(obj);
		}

		virtual public IList<T> Select()
		{
			return this.genericRepository.Query().ToList();
		}

        virtual public IList<T> Select(Expression<Func<T, Boolean>> expression)
        {
            return this.genericRepository.Query().Where(expression).ToList();
        }

        virtual public IList<T> Select(Expression<Func<T, Boolean>> expression, out Int32 totalItemCount)
        {
            totalItemCount = 0;
            return this.genericRepository.Query().Where(expression).ToList();
        }

        internal T SelectById(object id)
        {
            return this.genericRepository.SelectById(id);
        }
    }
}
