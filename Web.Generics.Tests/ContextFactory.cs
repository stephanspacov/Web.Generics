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

﻿using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.Infrastructure.DataAccess.FluentNHibernate;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.Tests.Repositories;
using Inspira.Blog.Infrastructure.DataAccess.FluentNHibernate;
using Inspira.Blog.Infrastructure.InversionOfControl;
using System.Reflection;

namespace Web.Generics.Tests
{
	internal class ContextFactory
	{
		internal static IRepositoryContext GetContext()
		{
			NHibernateSessionFactoryConfig.ConfigFilePath = @"..\..\..\Web.Generics.Tests\hibernate.cfg.xml";
			NHibernateSessionFactoryConfig.RepositoryType = typeof(PostRepository);

			var context = new NHibernateRepositoryContext();

			//var context = new EntityFrameworkRepositoryContext(new BlogContext());

			return context;
		}

        internal static void InitializeAppManager() {
            ApplicationManager.Initialize(new ApplicationConfiguration
            {
                DomainAssembly = Assembly.Load("Inspira.Blog.DomainModel"),
                Fluent = new ApplicationConfiguration.FluentConfiguration
                {
                    OverrideAssembly = Assembly.Load("Inspira.Blog"),
                    MappingConfigurationInstance = new MappingConfiguration()
                },
                InversionOfControl = new ApplicationConfiguration.InversionOfControlConfiguration { MapperInstance = new InversionOfControlMapper() },
                NHibernate = new ApplicationConfiguration.NHibernateConfiguration
                {
                    ConfigurationFilePath = @"..\..\hibernate.cfg.xml"
                }
            });
        }
	}
}
