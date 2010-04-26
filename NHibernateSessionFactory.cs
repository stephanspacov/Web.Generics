using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Web.Generics
{
    public static class NHibernateSessionFactoryConfig
    {
        public static Boolean UseFluentNHibernate = true;
    }

    public static class NHibernateSessionFactory<T>
    {
        public static String ConfigFilePath { get; set; }
        public static Type RepositoryType { get; set; }

        public static ISession OpenSession()
        {
            if (NHibernateSessionFactoryConfig.UseFluentNHibernate)
            {
                FluentNHibernate.FluentNHibernateHelper<T>.RepositoryType = RepositoryType;
                return FluentNHibernate.FluentNHibernateHelper<T>.OpenSession();
            }
            else
            {
                return NHibernateHelper<T>.OpenSession();
            }
        }
    }
}
