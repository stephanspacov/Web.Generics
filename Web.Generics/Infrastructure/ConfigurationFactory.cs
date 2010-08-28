using System;
using NHibernate;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.ApplicationServices.InversionOfControl;
using Web.Generics.Infrastructure.DataAccess.EntityFramework;
using Web.Generics.Infrastructure.DataAccess.FluentNHibernate;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.Infrastructure.Logging;
using System.Web;
using Web.Generics.Infrastructure.InversionOfControl.Unity;

namespace Web.Generics.Infrastructure
{
	public class ConfigurationFactory
	{
//		private static IControllerFactory controllerFactory;
        private static IInversionOfControlContainer container = null;
		private static ConfigurationFactory instance = null;

		private ConfigurationFactory()
		{
		}

		public static void Initialize<T>(InversionOfControlContainer containerType, IInversionOfControlMapper mapper)
		{
			if (containerType  == InversionOfControlContainer.Unity)
			{
				container = new UnityInversionOfControlContainer();

				// TODO: auto-discover repositories (boolean)
				var autoDiscoverRepositories = false;
				if (autoDiscoverRepositories) {
					foreach (Type entityType in typeof(T).Assembly.GetTypes()) {
                        if(entityType.IsClass)
                        {
                            var interfaceType = typeof(IRepositoryContext).MakeGenericType(entityType);
					        var implementationType = typeof(EntityFrameworkRepositoryContext).MakeGenericType(entityType);
					        container.RegisterType(interfaceType, implementationType);
                        }
					}
				} else {
                    container.RegisterType<IRepositoryContext, NHibernateRepositoryContext>();
                    //NHibernateSessionFactoryConfig.RepositoryType = typeof(GenericRepository<T>);
                    container.RegisterInstance<ISession>(FluentNHibernateHelper<T>.OpenSession());
                    //container.RegisterType<IGenericRepository<T>, GenericEntityFrameworkRepository<T>>();
				}
				container.RegisterType<Web.Generics.Infrastructure.Logging.ILogger, Web.Generics.Infrastructure.Logging.Log4NetLogger>();
				if (mapper != null)
				{
					mapper.DefineMappings(container);
				}
			}
//			controllerFactory = new GenericControllerFactory<T>(container);

			Configure();
		}

		private static void Configure()
		{
			// IoC / DI
//			ControllerBuilder.Current.SetControllerFactory(controllerFactory);

			// logging
			// log4net.Config.XmlConfigurator.Configure();

            // minify
//            Route route = new Route("Util/{action}/{parameters}", new MvcRouteHandler()) {
//	            Defaults = new RouteValueDictionary(new { controller = "Util", parameters = UrlParameter.Optional })
			//	        };

			//            RouteTable.Routes.Insert(0, route);
		}

        public static IInversionOfControlContainer GetInversionOfControlContainer()
        {
            return container;
        }

		public static void Initialize<T>()
		{
			Initialize<T>(null);
		}

		public static void Initialize<T>(IInversionOfControlMapper mapper)
		{
			Initialize<T>(InversionOfControlContainer.Unity, mapper);
		}
	}
}