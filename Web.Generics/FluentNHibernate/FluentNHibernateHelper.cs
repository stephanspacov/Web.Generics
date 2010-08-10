using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Automapping;
using NHibernate.Tool.hbm2ddl;
using System.IO;
using System;
using FluentNHibernate.Conventions.Helpers;
using System.Diagnostics;
using Web.Generics.Infrastructure.Logging;
using System.Web;

namespace Web.Generics.FluentNHibernate
{
    public class FluentNHibernateHelper<T>
    {
        static ILogger logger = new Log4NetLogger("Web_Generics");
        private static ISessionFactory SessionFactory {
            get { return (ISessionFactory)HttpContext.Current.Application["SessionFactory"]; }
            set { HttpContext.Current.Application["SessionFactory"] = value; }
        }

        public static Type RepositoryType { get; set; }

        private static ISessionFactory CreateSessionFactory()
        {
            var configuration = new Configuration();
            
            if (NHibernateSessionFactoryConfig.ConfigFilePath == null)
            {
                logger.Debug("FluentNHibernateHelper.CreateSessionFactory - configurando fluent");
                configuration.Configure();
            }
            else
            {
                logger.Debug("FluentNHibernateHelper.CreateSessionFactory - configurando fluent (com path): {0}", NHibernateSessionFactoryConfig.ConfigFilePath);
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
                                DefaultLazy.Always(),
                                new ColumnNullabilityConvention(),
                                new ForeignKeyConstraintNameConvention(),
                                new StringColumnLengthConvention(),
                                new EnumConvention(),
                                ForeignKey.EndsWith("_ID"),
                                ConventionBuilder.Reference.Always(x => x.Not.Nullable()),
                                ConventionBuilder.Reference.Always(x => x.Cascade.None()),
                                ConventionBuilder.Reference.Always(x => x.Not.LazyLoad()),
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

        internal static void DefineSessionFactory()
        {
            SessionFactory = CreateSessionFactory();
        }
    }
}