using NHibernate;
using NHibernate.Cfg;

namespace Web.Generics
{
	public class NHibernateHelper<T>
	{
		private static ISessionFactory _sessionFactory;
		private static ISessionFactory SessionFactory
		{
			get
			{
				if (_sessionFactory == null)
				{
					var configuration = new Configuration();
					configuration.Configure();
					configuration.AddAssembly(typeof(T).Assembly);
					_sessionFactory = configuration.BuildSessionFactory();
				}
				return _sessionFactory;
			}
		}

		public static ISession OpenSession()
		{
			return SessionFactory.OpenSession();
		}
	}
}