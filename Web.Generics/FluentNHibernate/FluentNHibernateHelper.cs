using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Automapping;
using NHibernate.Tool.hbm2ddl;
using System.IO;
using System;
using FluentNHibernate.Conventions.Helpers;

namespace Web.Generics.FluentNHibernate
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
                    Console.WriteLine("FluentNHibernateHelper.CreateSessionFactory: {0}", NHibernateSessionFactoryConfig.ConfigFilePath);
                    _sessionFactory = CreateSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static Type RepositoryType { get; set; }

        private static ISessionFactory CreateSessionFactory()
        {
            var configuration = new Configuration();
            if (NHibernateSessionFactoryConfig.ConfigFilePath == null)
            {
                configuration.Configure();
            }
            else
            {
                Console.WriteLine("FluentNHibernateHelper.CreateSessionFactory: {0}", NHibernateSessionFactoryConfig.ConfigFilePath);
                configuration.Configure(NHibernateSessionFactoryConfig.ConfigFilePath);
            }
            configuration.AddAssembly(typeof(T).Assembly);

            var autoMapping = AutoMap.AssemblyOf<T>()
                            .Alterations(x=>x.AddFromAssembly(RepositoryType.Assembly))
                            .Setup(s =>
                                s.FindIdentity =
                                    property => property.Name == "ID")
                            .Where(t=>t.BaseType != typeof(Exception))
                            .Conventions.Add(
                                PrimaryKey.Name.Is(pk=>"ID"),
                                DefaultCascade.SaveUpdate(),
                                DefaultLazy.Never(),
                                new ColumnNullabilityConvention(),
                                new ForeignKeyConstraintNameConvention(),
                                new StringColumnLengthConvention(),
                                new EnumConvention(),
                                ForeignKey.EndsWith("_ID"),
                                ConventionBuilder.Reference.Always(x=>x.Not.Nullable()),
                                ConventionBuilder.Reference.Always(x=>x.Cascade.None()),
                                ConventionBuilder.HasMany.Always(x=>x.LazyLoad()),
                                ConventionBuilder.HasMany.Always(x=>x.Inverse()),
                                ConventionBuilder.HasManyToMany.Always(x=>x.Table(x.TableName.Replace("ListTo", "").Substring(0, x.TableName.Length - 10)))
                            );

            autoMapping.GetType().GetMethod("UseOverridesFromAssemblyOf").MakeGenericMethod(RepositoryType).Invoke(autoMapping, null);

            return Fluently.Configure(configuration).Mappings(m => m.AutoMappings.Add(autoMapping)).BuildSessionFactory();
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