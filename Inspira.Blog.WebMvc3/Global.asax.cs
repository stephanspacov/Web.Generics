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