/*
Copyright 2010 Inspira Tecnologia.
All Rights Reserved.

Contact: Thiago Alves <thiago.alves@inspira.com.br>

This file is part of Web.Generics

Web.Generics is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Web.Generics is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with Web.Generics.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.ApplicationServices.InversionOfControl;
using Web.Generics.Infrastructure.DataAccess.EntityFramework;
using Web.Generics.Infrastructure.DataAccess.FluentNHibernate;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.Infrastructure.InversionOfControl.Unity;
using Web.Generics.Infrastructure.Logging;
using Web.Generics.Web.Mvc.Infrastructure;

namespace Web.Generics.Infrastructure
{
	public class ConfigurationFactory
	{
		private static IControllerFactory controllerFactory;
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
			controllerFactory = new GenericControllerFactory<T>(container);

			Configure();
		}

		private static void Configure()
		{
			// IoC / DI
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);

			// logging
			// log4net.Config.XmlConfigurator.Configure();

            // minify
            Route route = new Route("Util/{action}/{parameters}", new MvcRouteHandler()) {
	            Defaults = new RouteValueDictionary(new { controller = "Util", parameters = UrlParameter.Optional })
	        };

            RouteTable.Routes.Insert(0, route);
		}

        public static IInversionOfControlContainer GetInversionOfControlContainer()
        {
            return container;
        }
    }
}