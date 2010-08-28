using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.IO;
using System;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using System.Collections.Generic;

namespace Web.Generics.Infrastructure.DataAccess.FluentNHibernate
{
    public class FluentNHibernateHelper<T>
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = CreateSessionFactory();
                }
                return _sessionFactory;
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            var configuration = new Configuration();
            if (NHibernateSessionFactoryConfig.ConfigFilePath == null)
            {
                configuration.Configure();
            }
            else
            {
                configuration.Configure(NHibernateSessionFactoryConfig.ConfigFilePath);
            }
            configuration.AddAssembly(typeof(T).Assembly);

            return configuration.BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            //if (File.Exists(DbFile))
            //    File.Delete(DbFile);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config).Create(false, true);
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}