using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Web.Generics.Infrastructure.DataAccess.NHibernate;

namespace Web.Generics.ApplicationServices.DataAccess
{
	public class GenericNHibernateRepository<T> : GenericRepository<T> where T : class
	{
		protected readonly ISession session;
		public GenericNHibernateRepository(NHibernateRepositoryContext context): base(context)
		{
			this.session = context.Session;
		}
	}
}
