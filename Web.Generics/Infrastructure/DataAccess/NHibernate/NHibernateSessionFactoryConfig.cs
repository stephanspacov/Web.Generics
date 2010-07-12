using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.Infrastructure.DataAccess.NHibernate
{
    public class NHibernateSessionFactoryConfig
    {
        public static String ConfigFilePath { get; set; }

        public static Type RepositoryType { get; set; }

    }
}
