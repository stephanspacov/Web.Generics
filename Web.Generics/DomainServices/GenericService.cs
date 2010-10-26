using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Web.Generics.ApplicationServices.DataAccess;

namespace Web.Generics.DomainServices
{
	public class GenericService<T> where T : class
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
			this.genericRepository.SaveChanges();
		}

        virtual public void Delete(T obj)
		{
			this.genericRepository.Delete(obj);
			this.genericRepository.SaveChanges();
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
                sortProperty = dataRetrievalInfo.SortingInfo.GetSortExpression<T>();
                sortOrder = dataRetrievalInfo.SortingInfo.Order;
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

        virtual public IList<T> Select(Expression<Func<T, Boolean>> expression, IRowList rowList)
        {
            Int32 totalItemCount;
            var pagingInfo = rowList.PagingInfo;
            var sortingInfo = rowList.SortingInfo;
            var result = this.genericRepository.SelectWithPagingAndSorting(expression, pagingInfo.PageSize, pagingInfo.PageIndex, sortingInfo.GetSortExpression<T>(), sortingInfo.GetSortOrder(), out totalItemCount);
            pagingInfo.TotalItemCount = totalItemCount;
            return result;
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
