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

﻿using System.Data.Objects;
using Inspira.Blog.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.ApplicationServices.InversionOfControl;
using Web.Generics.Infrastructure;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.Tests.InversionOfControl;
using Web.Generics.Tests.Repositories;
using Inspira.Blog.Infrastructure.DataAccess.EntityFramework;
using Web.Generics.Web.Mvc;
using Web.Generics.UserInterface;

namespace Web.Generics.Tests
{
    [TestClass]
    public class Infrastructure_InversionOfControl_Tests
    {
        IInversionOfControlContainer container;

        [TestInitialize]
        public void Initialize()
        {
            NHibernateSessionFactoryConfig.ConfigFilePath = @"..\..\..\Web.Generics.Tests\hibernate.cfg.xml";
            NHibernateSessionFactoryConfig.RepositoryType = typeof(PostRepository);

            container = ApplicationManager.Container;
            //ConfigurationFactory.Initialize<Post>(InversionOfControlContainer.Unity, new MockMapper());
			//container = ConfigurationFactory.GetInversionOfControlContainer();

            //var nhibernateSession = FluentNHibernate.FluentNHibernateHelper<Post>.OpenSession();

            //container.RegisterType<IRepository<Post>, GenericRepository<Post>>();
            //container.RegisterType<IPostRepository, PostRepository>();
            //container.RegisterInstance<ISession>(nhibernateSession);
            //container.RegisterType<IRepositoryContext, NHibernateRepositoryContext>();
            //container.RegisterType<ObjectContext, BlogContext>();
        }

        [TestMethod]
        public void Register_Interface_And_Resolve_To_Implementation_Without_Chaining()
        {
            IMathPower impl = container.Resolve<PowerOfTwo>();
            Assert.AreEqual(16, impl.ElevateTo(4));
        }

        [TestMethod]
        public void Resolve_With_Chaining()
        {
            var mock = container.Resolve<MathMock>();
            Assert.AreEqual(16, mock.Power(4));
        }

        [TestMethod]
        public void Resolve_Controller_With_Generic_Implementations()
        {
            var controller = container.Resolve<GenericController<Post, GenericViewModel<Post>>>();
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void Resolve_Controller_With_Specific_Controller_And_ViewModel()
        {
            var controller = container.Resolve<PostController>();
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void Resolve_Controller_With_Generic_Controller_And_Specific_ViewModel()
        {
            var controller = container.Resolve<GenericController<Post, PostViewModel>>();
            Assert.IsNotNull(controller);
        }
    }
}
