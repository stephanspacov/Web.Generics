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
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using System.Web.Routing;
using Web.Generics.ApplicationServices;
using Web.Generics.ApplicationServices.InversionOfControl;
using Web.Generics.UserInterface;
using System.Reflection;

namespace Web.Generics.Web.Mvc.Infrastructure
{
    public class GenericControllerFactory : DefaultControllerFactory
	{
		private readonly IInversionOfControlContainer container;
        private readonly Assembly domainAssembly;
		public GenericControllerFactory(Assembly domainAssembly, IInversionOfControlContainer container)
		{
			this.container = container;
            this.domainAssembly = domainAssembly;
		}

		protected override Type GetControllerType(RequestContext requestContext, string controllerName)
		{
			Type controllerType = base.GetControllerType(requestContext, controllerName);
			if (controllerType == null)
			{
				// specific controller does not exist... try the generic one
                var entityType = domainAssembly.GetType(domainAssembly.GetTypes()[0].Namespace + "." + controllerName);
				if (entityType == null)
				{
					return null;
				}

				var genericViewModelType = typeof(GenericViewModel<>).MakeGenericType(entityType);
				controllerType = typeof(GenericController<,>).MakeGenericType(entityType, genericViewModelType);
			}
			return controllerType;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType != null) return container.Resolve(controllerType) as IController;
			return null;
		}

		public T Resolve<T>(T type)
		{
			return container.Resolve<T>();
		}
	}
}
