using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.UserInterface.HtmlHelpers;

namespace Web.Generics.DomainServices
{
	public class GenericService<T>
	{
        private readonly IRepository<T> genericRepository;
		public IRepository<T> GenericRepository
		{
			get
			{
				return this.genericRepository;
			}
		}

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

        virtual public IList<T> Select(DataRetrievalInfo<T> dataRetrievalInfo)
        {
            Int32? pageSize = null;
            Int32? pageIndex = null;

            if (dataRetrievalInfo.PagingInfo != null && dataRetrievalInfo.PagingInfo.PagingEnabled)
            {
                pageSize = dataRetrievalInfo.PagingInfo.PageSize;
                pageIndex = dataRetrievalInfo.PagingInfo.PageIndex;
            }

            Expression<Func<T, Object>> sortProperty = null;
            SortOrder? sortOrder = null;
            if (dataRetrievalInfo.SortingInfo != null && dataRetrievalInfo.SortingInfo.SortingEnabled)
            {
                sortProperty = dataRetrievalInfo.SortingInfo.SortProperty;
                sortOrder = dataRetrievalInfo.SortingInfo.SortOrder;
            }

            Int32 itemCount;
            var result = this.Select(dataRetrievalInfo.Filter, pageSize, pageIndex, sortProperty, sortOrder, out itemCount);

            dataRetrievalInfo.TotalItemCount = itemCount;

            return result;
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

		virtual public IList<T> Select(Expression<Func<T, Boolean>> expression, Int32? pageSize, Int32? pageIndex, Expression<Func<T, Object>> sortProperty, SortOrder? sortOrder,  out Int32 totalItemCount)
		{
			return this.genericRepository.SelectWithPagingAndSorting(expression, pageSize, pageIndex, sortProperty, sortOrder, out totalItemCount);
		}

        virtual public T SelectById(object id)
        {
            return this.genericRepository.SelectById(id);
        }

		[Obsolete("Use SaveOrUpdate()", false)]
		virtual public void Insert(T obj)
		{
			this.SaveOrUpdate(obj);
		}

		[Obsolete("Use SaveOrUpdate()", false)]
		virtual public void Update(T obj)
		{
			this.SaveOrUpdate(obj);
		}
	}
}
