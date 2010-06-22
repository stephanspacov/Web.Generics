using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Conventions;

namespace Web.Generics.FluentNHibernate
{
    public class ForeignKeyConstraintNameConvention
        : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.ForeignKey(
                String.Format("{0}_{1}_FK", instance.Member.Name, instance.EntityType.Name)
            );
        }
    }
}
