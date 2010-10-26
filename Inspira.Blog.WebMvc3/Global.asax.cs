using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Inspira.Blog.WebMvc3.Controllers;
using Web.Generics;
using System.Reflection;
using Inspira.Blog.Infrastructure.DataAccess.FluentNHibernate;
using Inspira.Blog.Infrastructure.InversionOfControl;

namespace Inspira.Blog.WebMvc3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new [] { typeof(HomeController).Namespace } 
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ApplicationManager.Initialize(Assembly.Load("Inspira.Blog.DomainModel"), Assembly.Load("Inspira.Blog"), new MappingConfiguration(), new InversionOfControlMapper());
            MvcApplicationManager.DefineControllerFactory();
        }

        protected void Application_BeginRequest()
        {
            AspNetApplicationManager.BindSessionToCurrentContext();
        }

        protected void Application_EndRequest()
        {
            AspNetApplicationManager.BindSessionToCurrentContext();
        }
    }
}