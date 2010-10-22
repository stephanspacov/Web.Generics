using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.Infrastructure.DataAccess.FluentNHibernate;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.Tests.Repositories;
using Inspira.Blog.Infrastructure.DataAccess.FluentNHibernate;
using Inspira.Blog.Infrastructure.InversionOfControl;
using System.Reflection;

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

        internal static void InitializeAppManager() {
            ApplicationManager.Initialize(new ApplicationConfiguration
            {
                DomainAssembly = Assembly.Load("Inspira.Blog.DomainModel"),
                Fluent = new ApplicationConfiguration.FluentConfiguration
                {
                    OverrideAssembly = Assembly.Load("Inspira.Blog"),
                    MappingConfigurationInstance = new MappingConfiguration()
                },
                InversionOfControl = new ApplicationConfiguration.InversionOfControlConfiguration { MapperInstance = new InversionOfControlMapper() },
                NHibernate = new ApplicationConfiguration.NHibernateConfiguration
                {
                    ConfigurationFilePath = @"..\..\hibernate.cfg.xml"
                }
            });
        }
	}
}
