using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Web.Generics.UserInterface.HtmlHelpers;

namespace Web.Generics.ApplicationServices.DataAccess
{
    public class GenericRepository<T> : IRepository<T>, IQueryable<T> where T : class
    {
        protected readonly IRepositoryContext context;
        public GenericRepository(IRepositoryContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<T> Query()
        {
            return this.context.Query<T>();
        }

		public virtual void SaveOrUpdate(T obj)
        {
            this.context.SaveOrUpdate(obj);
        }

		public virtual void Delete(T obj)
        {
            this.context.Delete(obj);
        }

		public virtual void Delete(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            this.context.Delete(filter);
        }

		public virtual void SaveChanges()
        {
            this.context.SaveChanges();
        }

		public virtual T SelectById(object id)
        {
            return this.context.SelectById<T>(id);
        }

		public virtual IEnumerator<T> GetEnumerator()
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

		public virtual IList<T> SelectWithPagingAndSorting(Expression<Func<T, bool>> expression, int? pageSize, int? pageIndex, Expression<Func<T, object>> sortProperty, UserInterface.HtmlHelpers.SortOrder? sortOrder, out int totalItemCount)
		{
			var query = this.Query().Where(expression);

			if (sortProperty != null)
			{
				if (sortOrder == SortOrder.Descending)
				{
					query = query.OrderByDescending(sortProperty);
				}
				else
				{
					query = query.OrderBy(sortProperty);
				}
			}

			totalItemCount = query.Count();

			if (pageSize.HasValue)
			{
				var startIndex = pageSize * (pageIndex - 1);
				query = query.Skip(startIndex.Value).Take(pageSize.Value);
			}

			var pagedList = query.ToList();
			return pagedList;
		}
	}
}
