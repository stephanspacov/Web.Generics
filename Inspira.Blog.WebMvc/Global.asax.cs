using System.Web.Mvc;
using System.Web.Routing;
using Inspira.Blog.DomainModel;
using Inspira.Blog.DomainServices;
using Inspira.Blog.Infrastructure.InversionOfControl;
using Web.Generics.ApplicationServices.InversionOfControl;
using Web.Generics.Infrastructure;
using Web.Generics.Infrastructure.DataAccess.NHibernate;

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
                new[] { "Inspira.Blog.WebMvc.Controllers" }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            NHibernateSessionFactoryConfig.RepositoryType = typeof(WebLogService);
            
			ConfigurationFactory.Initialize<WebLog>(InversionOfControlContainer.Unity, new InversionOfControlMapper());
        }
    }
}