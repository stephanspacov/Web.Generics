using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspira.Blog.DomainModel;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Automapping;

namespace Web.Generics.Tests.Repositories.NHibernate
{
    public class PostMappingOverride : IAutoMappingOverride<Post>
    {
        public void Override(AutoMapping<Post> mapping)
        {
            mapping.Map(p => p.PublishedAt).Nullable();
        }
    }
}
