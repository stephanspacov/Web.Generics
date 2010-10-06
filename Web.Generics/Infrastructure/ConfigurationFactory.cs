using System;
using System.Web.Mvc;

using Web.Generics.ApplicationServices;
using Web.Generics.Infrastructure.InversionOfControl;
using Web.Generics.Infrastructure.InversionOfControl.Unity;
using Web.Generics.Infrastructure.Logging;
using Web.Generics.FluentNHibernate;

namespace Web.Generics.Infrastructure
{
	public class ConfigurationFactory<T> where T:class
	{
		private readonly IControllerFactory controllerFactory;
		public ConfigurationFactory(InversionOfControlContainer containerType, IInversionOfControlMapper mapper)
		{
			IInversionOfControlContainer container = null;
			if (containerType  == InversionOfControlContainer.Unity)
			{
				container = new UnityInversionOfControlContainer();

				// TODO: repositório genérico
				// TODO: auto-discover repositories (boolean)

				var autoDiscoverRepositories = true;
				if (autoDiscoverRepositories) {
					foreach (Type entityType in typeof(T).Assembly.GetTypes()) {
                        if(entityType.IsClass)
                        {
					        var interfaceType = typeof(IGenericRepository<>).MakeGenericType(entityType);
					        var implementationType = typeof(GenericNHibernateRepository<>).MakeGenericType(entityType);
					        container.RegisterType(interfaceType, implementationType);
                        }
					}
				}else {
                    //container.RegisterType<IGenericRepository<T>, GenericEntityFrameworkRepository<T>>();
				}
				container.RegisterType<ILogger, Log4NetLogger>();
				mapper.DefineMappings(container);
			}
			this.controllerFactory = new GenericControllerFactory<T>(container);
		}

		public void Configure()
		{
			// IoC / DI
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);

			// logging
			// log4net.Config.XmlConfigurator.Configure();

            //FluentNHibernateHelper<T>.DefineSessionFactory();
		}
	}
}