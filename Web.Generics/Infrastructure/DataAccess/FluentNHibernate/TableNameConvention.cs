using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions;

namespace Web.Generics.Infrastructure.DataAccess.FluentNHibernate
{
    public class TableNameConvention : ManyToManyTableNameConvention
    {
        protected override string GetBiDirectionalTableName(global::FluentNHibernate.Conventions.Inspections.IManyToManyCollectionInspector collection, global::FluentNHibernate.Conventions.Inspections.IManyToManyCollectionInspector otherSide)
        {
            return collection.TableName;
        }

        protected override string GetUniDirectionalTableName(global::FluentNHibernate.Conventions.Inspections.IManyToManyCollectionInspector collection)
        {
            return collection.TableName;
        }
    }
}
