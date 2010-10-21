using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping.Alterations;
using Inspira.Blog.DomainModel;
using FluentNHibernate.Automapping;

namespace Inspira.Blog.Infrastructure.DataAccess.FluentNHibernate.Overrides
{
    public class WebLogOverride : IAutoMappingOverride<WebLog>
    {
        public void Override(AutoMapping<WebLog> mapping)
        {
            mapping.HasManyToMany(w => w.Collaborators).Table("User_WebLog").Cascade.Delete();
            mapping.References(w => w.Creator);
            mapping.Map(w => w.CreatedAt).Nullable();
            mapping.HasMany(w => w.Posts).Cascade.Delete();
            mapping.Map(w => w.Title).Nullable();

        }
    }
}
