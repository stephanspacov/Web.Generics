using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspira.Blog.DomainModel;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Automapping;

namespace Inspira.Blog.Infrastructure.DataAccess.FluentNHibernate
{
    public class UserMappingOverride : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.Table("[User]");
            mapping.HasManyToMany(x => x.Blogs).Table("User_WebLog");
        }
    }
}
