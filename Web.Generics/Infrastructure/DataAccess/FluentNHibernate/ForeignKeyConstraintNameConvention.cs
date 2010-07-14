using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Conventions;

namespace Web.Generics.Infrastructure.DataAccess.FluentNHibernate
{
    public class ForeignKeyConstraintNameConvention : IHasManyConvention, IHasManyToManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.ForeignKey(
                String.Format("FK_{0}_{1}", instance.Member.Name, instance.EntityType.Name)
            );
        }

        public void Apply(IManyToManyCollectionInstance instance)
        {
            instance.Key.ForeignKey(
                String.Format("FK_{0}_{1}", instance.Member.Name, instance.EntityType.Name)
            );
        }
    }
}
