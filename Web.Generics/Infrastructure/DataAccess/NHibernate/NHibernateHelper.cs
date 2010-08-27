using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace Web.Generics.Infrastructure.DataAccess.NHibernate
{
    public static class NHibernateHelper
    {
        private static ISessionFactory CreateSessionFactory()
        {
            var configuration = new Configuration();
            configuration.Configure();
            return configuration.BuildSessionFactory();
        }
    }
}
