using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping.Alterations;
using Inspira.Blog.DomainModel;
using FluentNHibernate.Automapping;

namespace Inspira.Blog.Infrastructure.DataAccess.FluentNHibernate
{
    public class WebLogMappingOverride : IAutoMappingOverride<WebLog>
    {
        public void Override(AutoMapping<WebLog> mapping)
        {
            mapping.HasManyToMany(x => x.Owners).Table("User_Blog");
        }
    }
}
