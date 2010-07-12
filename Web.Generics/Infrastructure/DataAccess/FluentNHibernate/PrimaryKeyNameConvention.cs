using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Web.Generics.FluentNHibernate
{
    public class PrimaryKeyNameConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column("ID");
        }
    }
}
