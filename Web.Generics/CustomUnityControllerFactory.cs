using System;
using System.Web.Mvc;
using System.ComponentModel.Design;
using System.Web;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using System.Reflection;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

namespace Web.Generics
{
	public class CustomUnityControllerFactory : DefaultControllerFactory
	{
		private readonly IUnityContainer container;

		public CustomUnityControllerFactory(String configFilePath) : this (configFilePath, true) {}

		public CustomUnityControllerFactory(String configFilePath, Boolean autoConfig)
		{
			var container = new UnityContainer();

			ExeConfigurationFileMap map = new ExeConfigurationFileMap();
			map.ExeConfigFilename = configFilePath;
			System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

			UnityConfigurationSection section = (UnityConfigurationSection)config.GetSection("unity");

			if (autoConfig)
			{
				for (Int32 i = 0; i < section.Containers[0].Types.Count; i++)
				{
					var type = section.Containers[0].Types[i].Type;
					var mapToType = section.Containers[0].Types[i].MapTo;

					foreach (Type implementationType in mapToType.Assembly.GetTypes())
					{
						if (implementationType == mapToType) continue;

						if (implementationType.Namespace == mapToType.Namespace)
						{
							foreach (var interfaceType in implementationType.GetInterfaces())
							{
								if (interfaceType.Namespace == type.Namespace)
								{
									container.RegisterType(interfaceType, implementationType);
								}
							}
						}
					}
				 }
			 }

			section.Containers[0].Configure(container);
			this.container = container;
		}

		public CustomUnityControllerFactory(IUnityMappingFactory mappingFactory)
		{
			var container = new UnityContainer();
            mappingFactory.GetMappings(container);
			this.container = container;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType != null)   return container.Resolve(controllerType) as IController;
			else return null;
		}

        internal object Resolve(Type type)
        {
            return container.Resolve(type);
        }
    }
}