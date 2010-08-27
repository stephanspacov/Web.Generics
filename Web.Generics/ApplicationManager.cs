using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.ApplicationServices.InversionOfControl;
using System.Diagnostics;
using System.Web;
using NHibernate;
using NHibernate.Context;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Automapping;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.Infrastructure.Logging;
using Web.Generics.Infrastructure.InversionOfControl.Unity;

namespace Web.Generics
{
    public static partial class ApplicationManager
    {
        private static ISessionFactory sessionFactory;
        private static ApplicationConfiguration applicationConfiguration;
        private static IInversionOfControlContainer container;

        public static void Initialize(ApplicationConfiguration config)
        {
            applicationConfiguration = config;

            sessionFactory = CreateSessionFactory();
            container = new UnityInversionOfControlContainer();

            var domainAssembly = config.DomainAssembly;

            var autoDiscoverRepositories = false;
            if (autoDiscoverRepositories)
            {
                foreach (Type entityType in domainAssembly.GetTypes())
                {
                    if (entityType.IsClass)
                    {
                        var interfaceType = typeof(IRepositoryContext).MakeGenericType(entityType);
                        var implementationType = typeof(NHibernateRepositoryContext).MakeGenericType(entityType);
                        container.RegisterType(interfaceType, implementationType);
                    }
                }
            }
            else
            {
                //NHibernateSessionFactoryConfig.RepositoryType = typeof(GenericRepository<T>);
                //container.RegisterInstance<ISession>(FluentNHibernateHelper<T>.OpenSession());
                //container.RegisterType<IGenericRepository<T>, GenericEntityFrameworkRepository<T>>();
            }
            container.RegisterType<ILogger, Log4NetLogger>();

            var mapper = config.InversionOfControl.MapperInstance;
            if (mapper != null)
            {
                mapper.DefineMappings(container);
            }

            // DefineControllerFactory(domainAssembly, container);
        }

        public static void Configure(IInversionOfControlMapper mapper)
        {
            //Configure<NHibernateRepositoryContext, UnityInversionOfControlContainer>();
        }

        public static void Configure<RepositoryContextT, InversionOfControlT>(IInversionOfControlMapper mapper)
        {
            //// define contexto padrão para os repositórios
            //container.RegisterInstance<IRepositoryContext>(repositoryContext);

            //ControllerBuilder.Current.SetControllerFactory(new GenericControllerFactory(container));

            //if (repositoryContext is NHibernateRepositoryContext)
            //{
            //    container.RegisterInstance<ISession>(FluentNHibernateHelper.OpenSession());
            //}

            //// chama custom mapper (IoC)
            //mapper.DefineMappings(container);
        }

        private static ISessionFactory CreateSessionFactory()
        {
            Trace.WriteLine(DateTime.Now + "    Creating a new session factory", "NHTests");

            var nhConfiguration = new Configuration();
            nhConfiguration.Configure();
            //return configuration.BuildSessionFactory();

            var fluentConfiguration = applicationConfiguration.Fluent.MappingConfigurationInstance;
            var assembly = applicationConfiguration.DomainAssembly;

            var sessionFactory = Fluently.Configure(nhConfiguration)
                .Mappings(m => m.AutoMappings.Add(AutoMap.Assembly(assembly, fluentConfiguration))).BuildSessionFactory();

            return sessionFactory;
        }

        public static ISession GetCurrentSession()
        {
            return sessionFactory.GetCurrentSession();
        }

    }
}
