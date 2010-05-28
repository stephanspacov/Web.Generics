using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Automapping;
using NHibernate.Tool.hbm2ddl;
using System.IO;
using System;
using FluentNHibernate.Conventions.Helpers;

namespace Web.Generics
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

        public static String ConfigFilePath { get; set; }

        private static ISessionFactory CreateSessionFactory()
        {
            var configuration = new Configuration();
            if (ConfigFilePath == null)
            {
                configuration.Configure();
            }
            else
            {
                configuration.Configure(ConfigFilePath);
            }
            configuration.AddAssembly(typeof(T).Assembly);

            return Fluently.Configure(configuration)
                //.Database(SQLiteConfiguration.Standard.UsingFile(DbFile))
                //.Database(MsSqlConfiguration.MsSql2005.ConnectionString(@"Data Source=localhost\SQLEXPRESS;Database=Teste.MigrationDotNet;Trusted_Connection=true"))
                .Mappings(m =>
                      m.AutoMappings.Add(
                        AutoMap.AssemblyOf<T>()
                            .Conventions.Add(
                                DefaultCascade.All(),
                                DefaultLazy.Never()
                            )
                      )
                 )
                //m.FluentMappings.AddFromAssemblyOf<Program>())
                //.ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
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