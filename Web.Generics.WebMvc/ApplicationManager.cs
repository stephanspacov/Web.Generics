using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.Web.Mvc.Infrastructure;
using System.Web.Mvc;
using System.Reflection;
using Web.Generics.ApplicationServices.InversionOfControl;

namespace Web.Generics
{
    public static partial class MvcApplicationManager
    {
        public static void DefineControllerFactory()
        {
			var domainAssembly = ApplicationManager.ApplicationConfiguration.DomainAssembly;
			var container = ApplicationManager.Container;
            var controllerFactory = new GenericControllerFactory(domainAssembly, container);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}
