using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Web.Generics.ApplicationServices.DataAccess;
using NHibernate.Context;

namespace Web.Generics.Infrastructure.DataAccess.NHibernate
{
    public class NHibernateRepositoryTestContext : NHibernateRepositoryContext
    {
        public NHibernateRepositoryTestContext(ISession session)
            : base(session)
        {
        }
    }
}