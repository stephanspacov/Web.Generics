using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics
{
    public static class NHibernateSessionFactoryConfig
    {
        public static Boolean UseFluentNHibernate = true;
        public static String SchemaGenerationDirectory;
        public static String ConfigFilePath;
        public static Type RepositoryType;
    }
}
