using System.Web.Mvc;
using System.Web.Routing;
using Inspira.Blog.DomainModel;
using Inspira.Blog.DomainServices;
using Inspira.Blog.Infrastructure.InversionOfControl;
using Web.Generics.Infrastructure;
using Web.Generics;
using Web.Generics.Infrastructure.Logging;
using Inspira.Blog.Infrastructure.DataAccess.FluentNHibernate;

namespace Inspira.Blog.WebMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "Inspira.Blog.WebMvc.Controllers", "Web.Generics.UserInterface.Controllers" }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

			ApplicationManager.Initialize(new ApplicationConfiguration
			{
				DomainAssembly = typeof(WebLog).Assembly,
				Fluent = new ApplicationConfiguration.FluentConfiguration
				{
					MappingConfigurationInstance = new MappingConfiguration(),
				},
				InversionOfControl = new ApplicationConfiguration.InversionOfControlConfiguration
				{
					MapperInstance = new InversionOfControlMapper(),
				}
			});
			MvcApplicationManager.DefineControllerFactory();
        }

		protected void Application_BeginRequest()
		{
			AspNetApplicationManager.BindSessionToCurrentContext();
		}

		protected void Application_EndRequest()
		{
			AspNetApplicationManager.UnbindSession();
		}
    }
}