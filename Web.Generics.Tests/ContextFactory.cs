using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.Infrastructure.DataAccess.FluentNHibernate;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.Tests.Repositories;

namespace Web.Generics.Tests
{
	internal class ContextFactory
	{
		internal static IRepositoryContext GetContext()
		{
			NHibernateSessionFactoryConfig.ConfigFilePath = @"..\..\..\Web.Generics.Tests\hibernate.cfg.xml";
			NHibernateSessionFactoryConfig.RepositoryType = typeof(PostRepository);

			var nhibernateSession = FluentNHibernateHelper<Post>.OpenSession();
			var context = new NHibernateRepositoryContext(nhibernateSession);

			//var context = new EntityFrameworkRepositoryContext(new BlogContext());

			return context;
		}
	}
}
