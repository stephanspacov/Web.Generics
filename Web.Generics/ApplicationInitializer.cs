using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.ApplicationServices.InversionOfControl;
using Web.Generics.Infrastructure.DataAccess;
using Web.Generics.Infrastructure.InversionOfControl.Unity;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.Infrastructure.InversionOfControl;
using System.Web.Mvc;
using Web.Generics.Infrastructure.Authentication;
using NHibernate;
using Web.Generics.FluentNHibernate;

namespace Web.Generics
{
    public static class ApplicationInitializer
    {
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
    }
}
