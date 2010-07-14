using System;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.ApplicationServices.InversionOfControl;
using Web.Generics.Infrastructure.Authentication;
using Web.Generics.Infrastructure.DataAccess.EntityFramework;
using Web.Generics.Infrastructure.DataAccess.FluentNHibernate;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.Infrastructure.InversionOfControl.Unity;
using Web.Generics.Infrastructure.Logging;

namespace Web.Generics.Infrastructure
{
	public class ConfigurationFactory<T> where T:class
	{
		private readonly IControllerFactory controllerFactory;
        private readonly IInversionOfControlContainer container = null;
		public ConfigurationFactory(InversionOfControlContainer containerType, IInversionOfControlMapper mapper)
		{
			if (containerType  == InversionOfControlContainer.Unity)
			{
				container = new UnityInversionOfControlContainer();

				// TODO: repositório genérico
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
				container.RegisterType<ILogger, Log4NetLogger>();
				mapper.DefineMappings(container);
			}
			this.controllerFactory = new GenericControllerFactory(container);
		}

		public void Configure()
		{
			// IoC / DI
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);

			// logging
			// log4net.Config.XmlConfigurator.Configure();

            Route route = new Route("Util/{action}/{parameters}", new MvcRouteHandler()) {
	            Defaults = new RouteValueDictionary(new { controller = "Util", parameters = UrlParameter.Optional })
	        };

            RouteTable.Routes.Insert(0, route);
		}

        public IInversionOfControlContainer GetInversionOfControlContainer()
        {
            return container;
        }
    }
}