using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Diagnostics;
using Web.Generics.Infrastructure.Logging;
using System.Web;

namespace Web.Generics
{
    public static class NHibernateSessionFactory<T>
    {
        private const String NHIBERNATE_SESSION_KEY = "__NHibernateSession__";
        static ILogger logger = new Log4NetLogger("Web_Generics");

        public static ISession OpenSession()
        {
            if (HttpContext.Current == null)
            {
                return CreateNewSession(); // para projetos não-web
            }
            if (!HttpContext.Current.Items.Contains(NHIBERNATE_SESSION_KEY))
            {           
                HttpContext.Current.Items.Add(NHIBERNATE_SESSION_KEY, CreateNewSession());
            }
            return (ISession)HttpContext.Current.Items[NHIBERNATE_SESSION_KEY];
        }

        public static void CloseCurrentSession()
        {
            ISession session = (ISession)HttpContext.Current.Items[NHIBERNATE_SESSION_KEY];
            if (session != null)
            {
                session.Close();
            }
        }

        private static ISession CreateNewSession()
        {
            if (NHibernateSessionFactoryConfig.UseFluentNHibernate)
            {
                logger.Debug(" >>>> Abrindo session (com fluent)");
                return FluentNHibernate.FluentNHibernateHelper<T>.OpenSession();
            }
            else
            {
                logger.Debug(" >>>> Abrindo session (sem fluent)");
                return NHibernateHelper<T>.OpenSession();
            }
        }
    }
}
