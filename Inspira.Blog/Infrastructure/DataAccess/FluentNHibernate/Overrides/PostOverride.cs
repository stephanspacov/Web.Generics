using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping.Alterations;
using Inspira.Blog.DomainModel;
using FluentNHibernate.Automapping;
using FluentNHibernate;

namespace Inspira.Blog.Infrastructure.DataAccess.FluentNHibernate.Overrides
{
    public class PostOverride : IAutoMappingOverride<Post>
    {
        public void Override(AutoMapping<Post> mapping)
        {
            mapping.HasManyToMany(p => p.Tags).Table("Post_Tag");
        }
    }
}
