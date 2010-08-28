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

			var context = new NHibernateRepositoryContext();

			//var context = new EntityFrameworkRepositoryContext(new BlogContext());

			return context;
		}
	}
}
