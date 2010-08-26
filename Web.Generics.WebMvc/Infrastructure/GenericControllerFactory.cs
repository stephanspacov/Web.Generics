using System;
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
