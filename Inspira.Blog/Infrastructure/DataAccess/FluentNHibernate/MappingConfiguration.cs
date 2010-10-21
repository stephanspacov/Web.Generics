using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using Inspira.Blog.DomainModel;

namespace Inspira.Blog.Infrastructure.DataAccess.FluentNHibernate
{
	public class MappingConfiguration : DefaultAutomappingConfiguration
	{
        public override bool IsComponent(Type type)
        {
            return type == typeof(Address);
        }
	}
}
