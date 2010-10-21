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
            mapping.HasManyToMany(p => p.Tags).Table("Post_Tag");
			mapping.Map(p => p.PublishedAt).Nullable();
			mapping.Map(p => p.Title).Length(255);
			mapping.Map(p => p.Text).Length(4000);
        }
    }
}
