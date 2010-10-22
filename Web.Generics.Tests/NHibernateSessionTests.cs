using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using System.Reflection;
using Inspira.Blog.Infrastructure.InversionOfControl;
using Inspira.Blog.Infrastructure.DataAccess.FluentNHibernate;
using NHibernate.Context;
using Inspira.Blog.DomainModel;

namespace Web.Generics.Tests
{
    [TestClass]
    public class NHibernateSessionTests
    {
        [TestInitialize]
        public void Initialize()
        {


            var session = ApplicationManager.SessionFactory.OpenSession();
            var user = new User { Username="user", Name = "my name", Email = "thiago@inspira.com.br", Password = "******", Address = new Address { City = "São paulo", StreetName="name", Number="123B", State="SP", ZipCode="03423-234" } };
            user.AddBlog(new WebLog { Title = "My blog", Creator=user });
            session.Save(user);
            session.Flush();
            session.Close();

            session = ApplicationManager.SessionFactory.OpenSession();
            ThreadStaticSessionContext.Bind(session);
        }

        [TestMethod]
        public void Insert_child_does_not_load_children()
        {
            var session = ApplicationManager.GetCurrentSession();
            var user = session.Load<User>(1);
            user.AddBlog(new WebLog { Title = "My blog 22222", Creator = user });
            session.Flush();
        }

        [TestMethod]
        public void Insert_child_does_not_load_children_v2()
        {
            var session = ApplicationManager.GetCurrentSession();
            var webLog = new WebLog { Title = "My blog 22222", Creator = new User { ID=1 } };
            session.Save(webLog);
            session.Flush();
        }
    }
}
