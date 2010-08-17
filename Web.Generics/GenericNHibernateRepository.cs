using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using System.Reflection;
using Web.Generics.HtmlHelpers;
using System.Web;
using Web.Generics.ModelAttributes;

namespace Web.Generics
{
	public class GenericNHibernateRepository<T> : IGenericRepository<T> where T : class
	{
		ISession session;

		protected ISession Session
		{
			get { return this.session; }
		}

		public GenericNHibernateRepository()
		{
			if (HttpContext.Current == null)//
			{
				session = NHibernateSessionFactory<T>.OpenSession();
				return;
			}

			if (!HttpContext.Current.Items.Contains("NHibernateSession"))
			{
				session = NHibernateSessionFactory<T>.OpenSession();
				HttpContext.Current.Items.Add("NHibernateSession", session);
			}
			else
			{
				session = (ISession)HttpContext.Current.Items["NHibernateSession"];
			}
		}

		virtual public Int32 Insert(T obj)
		{
			using (ITransaction transaction = session.BeginTransaction())
			{
				session.Save(obj);
				transaction.Commit();
			}

			return 0;
		}

		virtual public Int32 Update(T obj)
		{
			using (ITransaction transaction = session.BeginTransaction())
			{
				session.Update(obj);
				transaction.Commit();
            }

			return 0;
		}

		virtual public Int32 Delete(T obj)
		{
			using (ITransaction transaction = session.BeginTransaction())
            {
                IdPropertyAttribute idProperty = (IdPropertyAttribute)obj.GetType().GetCustomAttributes(typeof(IdPropertyAttribute), true).SingleOrDefault();
                string idPropertyName = idProperty != null ? idProperty.PropertyName : "ID";
                obj = this.SelectById(obj.GetType().GetProperty(idPropertyName).GetValue(obj, null));
				session.Delete(obj);
				transaction.Commit();
            }

			return 0;
		}

		virtual public IList<T> Select()
		{
            return this.Select(null as IWebGrid);
		}

		virtual public IList<T> Select(IWebGrid parameters)
		{
			ICriteria criteria = session.CreateCriteria<T>();

			if (parameters != null)
			{
				CreateCriteriaForFilterParameters(parameters, ref criteria);
				CreatePagingAndSortingForFilterParameters(parameters, ref criteria);

				if (!String.IsNullOrEmpty(parameters.SearchQuery))
				{
					CreateFilterBySearchQueryForFilterParameters(parameters, ref criteria);
				}
			}
			
            IList<T> result = criteria.List<T>();

			return result;
		}

		virtual public Int32 Count(IWebGrid parameters)
		{
			ICriteria criteria = session.CreateCriteria<T>();
			if (parameters != null)
			{
				CreateCriteriaForFilterParameters(parameters, ref criteria);

				if (!String.IsNullOrEmpty(parameters.SearchQuery))
				{
					CreateFilterBySearchQueryForFilterParameters(parameters, ref criteria);
				}
			}

			criteria.SetProjection(Projections.RowCount());
			return ((Int32)criteria.List()[0]);
		}

		virtual public Int32 Count()
		{
			ICriteria criteria = session.CreateCriteria<T>();

			criteria.SetProjection(Projections.RowCount());

			return ((Int32)criteria.List()[0]);
		}
        
		virtual public T SelectById(Object id)
		{
			T result = session.Get<T>(id);

			return result;
		}

		private static void CreateAlias(ICriteria criteria, string columnName)
		{
			if (String.IsNullOrEmpty(columnName) || !columnName.Contains('.')) return;

			var aliasStack = new Stack<String>(columnName.Split('.'));

			while (aliasStack.Count > 0)
			{
				string referencedTableName = aliasStack.Pop();
				if (criteria.GetCriteriaByAlias(referencedTableName) == null)
				{
					criteria.CreateAlias(referencedTableName, referencedTableName);
				}
			}
		}

		private static void CreateFilterBySearchQueryForFilterParameters(IWebGrid parameters, ref ICriteria criteria)
		{
			var fieldsToSearch = typeof(T).GetProperties().Where(x => x.PropertyType == typeof(String)).Select(x => x.Name).ToList<String>();

			Disjunction disjunction = Restrictions.Disjunction();
			foreach (String columnName in fieldsToSearch)
			{
				CreateAlias(criteria, columnName);
				disjunction.Add(Restrictions.Like(columnName, parameters.SearchQuery, MatchMode.Anywhere));
			}

			criteria.Add(disjunction);
		}

		private static void CreatePagingAndSortingForFilterParameters(IWebGrid parameters, ref ICriteria criteria)
		{
			parameters.CorrectSortPropertyAndOrder();

			if (!String.IsNullOrEmpty(parameters.SortProperty))
			{
				if (parameters.SortOrder == SortOrder.Ascending)
				{
					criteria = criteria.AddOrder(Order.Asc(parameters.SortProperty));
				}
				else
				{
					criteria = criteria.AddOrder(Order.Desc(parameters.SortProperty));
				}
			}
			if (parameters.AllowPaging)
			{
				criteria = criteria.SetFirstResult(parameters.PageStartIndex).SetMaxResults(parameters.PageSize);
			}
		}

		private static void CreateCriteriaForFilterParameters(IWebGrid parameters, ref ICriteria criteria)
		{
			if (!String.IsNullOrEmpty(parameters.SortProperty) && parameters.SortProperty.Contains("."))
			{
				String alias = parameters.SortProperty.Split('.')[0];
				criteria = criteria.CreateAlias(alias, alias);
			}

			if (parameters.FilterConditions != null && parameters.FilterConditions.Count > 0)
			{
				foreach (FilterCondition condition in parameters.FilterConditions)
				{
					Func<String, Object, AbstractCriterion> expressionFunction = null;
					AbstractCriterion expression = null;

					String[] propertyStack = condition.Property.Split('.');
					Type propertyType = typeof(T);

					for (Int32 i = 0; i < propertyStack.Length; i++)
					{
						String property = propertyStack[i];

						try
						{
							var propertyValue = propertyType.GetProperty(property);

							propertyType = propertyValue.PropertyType;
							if (i + 1 < propertyStack.Length)
							{
								if (i > 0) criteria = criteria.GetCriteriaByAlias(propertyStack[i - 1]);

                                if (criteria.GetCriteriaByAlias(property) == null)
                                {
                                    criteria.CreateAlias(property, property);
                                }
							}
						}
						catch (NullReferenceException exc)
						{
							throw new ApplicationException("Propriedade não encontrada: " + property, exc);
						}
					}

					if (propertyType == typeof(Int32?))
						condition.Value = int.Parse(condition.Value.ToString());
					else if (propertyType == typeof(Boolean?))
						condition.Value = Boolean.Parse(condition.Value.ToString());
					else if (propertyType == typeof(DateTime?))
						condition.Value = DateTime.Parse(condition.Value.ToString());
					else if (propertyType == typeof(String))
						condition.Value = condition.Value.ToString();

					if (condition.Comparer == FilterCondition.ComparerType.eq)
					{
						if (condition.Value == null)
						{
							//expressionFunction = Restrictions.IsNull;
							expression = Restrictions.IsNull(condition.Property);
						}
						else
						{
							expressionFunction = Restrictions.Eq;
							expression = expressionFunction(condition.Property, condition.Value);
						}
					}
					else if (condition.Comparer == FilterCondition.ComparerType.neq)
					{
						expressionFunction = Restrictions.Eq;
						expression = Restrictions.Not(expressionFunction(condition.Property, condition.Value));
					}
					else if (condition.Comparer == FilterCondition.ComparerType.like)
					{
						expression = Restrictions.Like(condition.Property, condition.Value.ToString(), MatchMode.Anywhere);
					}
					else if (condition.Comparer == FilterCondition.ComparerType.slike)
					{
						expression = Restrictions.Like(condition.Property, condition.Value.ToString(), MatchMode.Start);
					}
					else if (condition.Comparer == FilterCondition.ComparerType.elike)
					{
						expression = Restrictions.Like(condition.Property, condition.Value.ToString(), MatchMode.End);
					}
					else if (condition.Comparer == FilterCondition.ComparerType.lt)
					{
						expressionFunction = Restrictions.Lt;
						expression = expressionFunction(condition.Property, condition.Value);
					}
					else if (condition.Comparer == FilterCondition.ComparerType.le)
					{
						expressionFunction = Restrictions.Le;
						expression = expressionFunction(condition.Property, condition.Value);
					}
					else if (condition.Comparer == FilterCondition.ComparerType.gt)
					{
						expressionFunction = Restrictions.Gt;
						expression = expressionFunction(condition.Property, condition.Value);
					}
					else if (condition.Comparer == FilterCondition.ComparerType.ge)
					{
						expressionFunction = Restrictions.Ge;
						expression = expressionFunction(condition.Property, condition.Value);
					}
					else
					{
						expressionFunction = Restrictions.Eq;
						expression = expressionFunction(condition.Property, condition.Value);
					}

					criteria = criteria.Add(expression);
				}
			}
		}

		public System.Collections.IList SelectByType(Type relatedEntityType)
		{
			return session.CreateCriteria(relatedEntityType).List();
		}

		public IList<T> Select(System.Linq.Expressions.Expression<Func<T, bool>> expr)
		{
			throw new NotImplementedException();
		}
	}
}
