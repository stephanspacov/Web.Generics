using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Inspira.Blog.DomainModel;
using Web.Generics.Infrastructure;
using Web.Generics.Infrastructure.InversionOfControl;
using Inspira.Blog.Infrastructure;
using Inspira.Blog.Infrastructure.InversionOfControl;
using Inspira.Blog.DomainServices;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.Infrastructure.InversionOfControl.Unity;
using Web.Generics;

namespace Inspira.Blog.WebMvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            ApplicationInitializer.Configure(new InversionOfControlMapper());
/*            ConfigurationFactory.Configure {
                InversionOfControlContainer = new UnityInversionOfControlContainer(),
                RepositoryContext = new NHibernateRepositoryContext(
            );
            */

            // NHibernateSessionFactoryConfig.RepositoryType = typeof(WebLogService);
            // new ConfigurationFactory<WebLog>(InversionOfControlContainer.Unity, new InversionOfControlMapper()).Configure();
        }

        
    }
}