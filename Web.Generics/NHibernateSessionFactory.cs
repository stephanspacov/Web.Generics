using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Diagnostics;
using Web.Generics.Infrastructure.Logging;

namespace Web.Generics
{
    public static class NHibernateSessionFactoryConfig
    {
        public static Boolean UseFluentNHibernate = true;
        public static String SchemaGenerationDirectory;
        public static String ConfigFilePath;
    }

    public static class NHibernateSessionFactory<T>
    {
        public static Type RepositoryType { get; set; }
        static ILogger logger = new Log4NetLogger("Web_Generics");

        public static ISession OpenSession()
        {
            if (NHibernateSessionFactoryConfig.UseFluentNHibernate)
            {
                logger.Debug(" >>>> Abrindo session (com fluent)");
                FluentNHibernate.FluentNHibernateHelper<T>.RepositoryType = RepositoryType;
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
