﻿using System;
using System.Web.Mvc;

using Web.Generics.ApplicationServices;
using Web.Generics.Infrastructure.InversionOfControl;
using Web.Generics.Infrastructure.InversionOfControl.Unity;
using Web.Generics.Infrastructure.Logging;

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
					foreach (var entityType in typeof(T).Assembly.GetTypes()) {
						var interfaceType = typeof(IGenericRepository<>).MakeGenericType(entityType);
						var implementationType = typeof(GenericNHibernateRepository<>).MakeGenericType(entityType);
						container.RegisterType(interfaceType, implementationType);
					}
				}else {
					container.RegisterType<IGenericRepository<T>, GenericNHibernateRepository<T>>();
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
		}
	}
}