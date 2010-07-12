using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspira.Blog.DomainModel;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Automapping;

namespace Inspira.Blog.Infrastructure.DataAccess.FluentNHibernate
{
    public class PostMappingOverride : IAutoMappingOverride<Post>
    {
        public void Override(AutoMapping<Post> mapping)
        {
            mapping.HasManyToMany(p => p.Tags).Table("PostTag");
            mapping.References(p => p.WebLog).Nullable();
        }
    }
}
