using System;
using System.Collections.Generic;
using System.Web;
using NHibernate;
using NHibernate.Criterion;

namespace Web.Generics
{
    public class GenericRepository<T>
    {
        private ISession session;

        public GenericRepository()
        {
            session = NHibernateHelper<T>.OpenSession();
        }

        #region Criteria

        public ICriteria GetAllCriteria()
        {
            ICriteria criteria = session.CreateCriteria(typeof(T));
            return criteria;
        }

        private void CreateAlias(ICriteria criteria, string columnName)
        {
            if (!String.IsNullOrEmpty(columnName))
            {
                if (columnName.Contains("."))
                {
                    string referencedTableName = columnName.Split('.').GetValue(0).ToString();
                    if (criteria.GetCriteriaByAlias(referencedTableName) == null)
                    {
                        criteria.CreateAlias(referencedTableName, referencedTableName);
                    }
                }
            }
        }

        private void CreateAlias(ICriteria criteria, string columnName, string referencedTableName)
        {
            if (!String.IsNullOrEmpty(columnName))
            {
                if (columnName.Contains("."))
                {
                    string alias = columnName.Split('.').GetValue(0).ToString();
                    if (criteria.GetCriteriaByAlias(referencedTableName) == null)
                    {
                        criteria.CreateAlias(referencedTableName, alias);
                    }
                }
            }
        }

        private void CreateAlias(ICriteria criteria, IList<String> columnNames)
        {
            foreach (String columnName in columnNames)
            {
                CreateAlias(criteria, columnName);
            }
        }

        private void OrderCriteria(ICriteria criteria, string propertyName, bool ascending)
        {
            CreateAlias(criteria, propertyName);
            Order order = new Order(propertyName, ascending);
            criteria.AddOrder(order);
        }

        private void PageCriteria(ICriteria criteria, int pageIndex, int pageSize)
        {
            criteria.SetFirstResult(pageIndex * pageSize);
            if (pageSize > 0)
            {
                criteria.SetMaxResults(pageSize);
            }
        }

        private void FilterCriteriaByForeignKey(ICriteria criteria, string foreignKeyColumnName, int foreignKeyColumnValue)
        {
            if (!String.IsNullOrEmpty(foreignKeyColumnName))
            {
                if (foreignKeyColumnValue != 0)
                {
                    CreateAlias(criteria, foreignKeyColumnName);
                    criteria.Add(Expression.Eq(foreignKeyColumnName, foreignKeyColumnValue));
                }
            }
        }

        private void FilterCriteriaByForeignKey(ICriteria criteria, string foreignKeyColumnName, int foreignKeyColumnValue, string tableName)
        {
            if (!String.IsNullOrEmpty(foreignKeyColumnName))
            {
                if (foreignKeyColumnValue != 0)
                {
                    CreateAlias(criteria, foreignKeyColumnName, tableName);
                    criteria.Add(Expression.Eq(foreignKeyColumnName, foreignKeyColumnValue));
                }
            }
        }

        private void FilterCriteriaByForeignKeys(ICriteria criteria, IList<String> foreignKeyColumnNames, IList<Int32> foreignKeyColumnValues)
        {
            if (foreignKeyColumnNames != null)
            {
                for (Int32 i = 0; i < foreignKeyColumnNames.Count; i++)
                {
                    FilterCriteriaByForeignKey(criteria, foreignKeyColumnNames[i], foreignKeyColumnValues[i]);
                }
            }
        }

        private void FilterCriteriaBySearchQuery(ICriteria criteria, String searchQuery, String columnToSearch)
        {
            if (!String.IsNullOrEmpty(searchQuery) && searchQuery.Trim().Length > 0)
            {
                CreateAlias(criteria, columnToSearch);
                criteria.Add(Restrictions.Like(columnToSearch, searchQuery, MatchMode.Anywhere));
            }
        }

        private void FilterCriteriaBySearchQuery(ICriteria criteria, String searchQuery, IList<String> columnsToSearch)
        {
            if (!String.IsNullOrEmpty(searchQuery) && searchQuery.Trim().Length > 0)
            {
                Disjunction disjunction = Restrictions.Disjunction();
                foreach (String columnName in columnsToSearch)
                {
                    CreateAlias(criteria, columnName);
                    disjunction.Add(Restrictions.Like(columnName, searchQuery, MatchMode.Anywhere));
                }
                criteria.Add(disjunction);
            }
        }

        private void FilterCriteriaBySearchQueryAndForeignKeys(ICriteria criteria, String searchQuery, IList<String> searchColumnNames, IList<String> referencedColumns, IList<Int32> foreignKeyColumnValues)
        {
            // filters
            FilterCriteriaByForeignKeys(criteria, referencedColumns, foreignKeyColumnValues);

            FilterCriteriaBySearchQuery(criteria, searchQuery, searchColumnNames);
        }

        #endregion

        #region IRepository<T> Members

        public void Save(T obj)
        {
            session.Save(obj);
        }

        public void Update(T obj)
        {
            session.Update(obj);
        }

        public void Delete(T obj)
        {
            session.Delete(obj);
        }

        public T Load(object id)
        {
            return session.Load<T>(id);
        }

        public T GetReference(object id)
        {
            return session.Get<T>(id);
        }

        public void DeleteAll(IList<T> objs)
        {
            for (Int32 i = 0; i < objs.Count; ++i)
            {
                Delete(objs[i]);
            }
        }

        public void UpdateAll(IList<T> objs)
        {
            for (Int32 i = 0; i < objs.Count; ++i)
            {
                Update(objs[i]);
            }
        }

        public void InsertAll(IList<T> objs)
        {
            for (Int32 i = 0; i < objs.Count; ++i)
            {
                Save(objs[i]);
            }
        }

        public void Detach(T item)
        {
            session.Evict(item);
        }

        internal void Flush()
        {
            session.Flush();
        }

        public IList<T> GetAll()
        {
            return GetAllCriteria().List<T>();
        }

        public void Commit()
        {
            if (session.Transaction.IsActive)
            {
                session.Transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (session.Transaction.IsActive)
            {
                session.Transaction.Rollback();
                session.Clear();
            }
        }

        public void BeginTransaction()
        {
            Rollback();
            session.BeginTransaction();
        }

        #endregion

        #region Other IRepository<T> Members


        public IList<T> GetAllOrdered(string propertyName, bool ascending)
        {
            ICriteria criteria = GetAllCriteria();
            OrderCriteria(criteria, propertyName, ascending);
            return criteria.List<T>();

        }

        public IList<T> GetPagedAndOrdered(int pageIndex, int pageSize, string sortProperty, Boolean isAscending)
        {
            ICriteria criteria = GetAllCriteria();
            CreateAlias(criteria, sortProperty);

            OrderCriteria(criteria, sortProperty, isAscending);
            PageCriteria(criteria, pageIndex, pageSize);
            return criteria.List<T>();
        }

        public IList<T> FilterPagedAndOrdered(String searchQuery, IList<String> searchColumnNames, IList<String> referencedColumns, IList<Int32> foreignKeyColumnValues)
        {
            return this.FilterPagedAndOrdered(searchQuery, searchColumnNames, referencedColumns, foreignKeyColumnValues, 0, 0, null, true);
        }


        public IList<T> FilterPagedAndOrdered(String searchQuery, IList<String> searchColumnNames, IList<String> referencedColumns, IList<Int32> foreignKeyColumnValues, int pageIndex, int pageSize, string sortProperty, Boolean isAscending)
        {
            ICriteria criteria = GetAllCriteria();

            FilterCriteriaBySearchQueryAndForeignKeys(criteria, searchQuery, searchColumnNames, referencedColumns, foreignKeyColumnValues);

            // paging & sorting
            OrderCriteria(criteria, sortProperty, isAscending);
            PageCriteria(criteria, pageIndex, pageSize);

            return criteria.List<T>();
        }

        public Int32 FilterAndCount(string searchQuery, IList<string> searchColumnNames, IList<string> referencedColumns, IList<int> foreignKeyColumnValues)
        {
            ICriteria criteria = GetAllCriteria();

            FilterCriteriaBySearchQueryAndForeignKeys(criteria, searchQuery, searchColumnNames, referencedColumns, foreignKeyColumnValues);

            criteria.SetProjection(Projections.RowCount());

			Object result = criteria.UniqueResult();
			return result == null ? 0 : (Int32)result;
        }

        public T FindByPrimaryKey(String column, Int32 value)
        {
            ICriteria criteria = session.CreateCriteria(typeof(T));
            criteria.Add(Expression.Eq(column, value));
            return criteria.UniqueResult<T>();
        }

        public IList<T> FindByForeignKey(String column, Int32 value)
        {
            ICriteria criteria = session.CreateCriteria(typeof(T));
            criteria.Add(Expression.Eq(column, value));
            return criteria.List<T>();
        }

        public T FindUniqueResultByColumnValue(String column, String value)
        {
            ICriteria criteria = session.CreateCriteria(typeof(T));
            criteria.Add(Expression.Eq(column, value));
            return criteria.UniqueResult<T>();
        }

        public T FindLastRowByForeignKey(String column, Int32 value)
        {
            ICriteria criteria = session.CreateCriteria(typeof(T));
            criteria.Add(Expression.Eq(column, value));
            Order cr1 = new Order("ID", false);
            criteria.AddOrder(cr1);
            return criteria.SetFirstResult(0).SetMaxResults(1).UniqueResult<T>();
        }

        public Int32 GetCount()
        {
            ICriteria criteria = session.CreateCriteria(typeof(T));
            criteria.SetProjection(Projections.RowCount());
            return ((Int32)criteria.List()[0]);
        }
        #endregion

        #region to delete?

        public IList<T> Find(IList<string> strs)
        {
            System.Collections.Generic.IList<ICriterion> objs = new System.Collections.Generic.List<ICriterion>();
            foreach (string s in strs)
            {
                ICriterion cr1 = Expression.Sql(s);
                objs.Add(cr1);
            }
            ICriteria criteria = session.CreateCriteria(typeof(T));
            foreach (ICriterion rest in objs)
                session.CreateCriteria(typeof(T)).Add(rest);

            criteria.SetFirstResult(0);
            return criteria.List<T>();
        }

        #endregion
    }
}
