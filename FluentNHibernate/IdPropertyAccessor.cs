using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Properties;

namespace Web.Generics.FluentNHibernate
{
    internal class IdPropertyAccessor : IPropertyAccessor
    {
        public bool CanAccessThroughReflectionOptimizer
        {
            get { return true; }
        }

        public IGetter GetGetter(Type theClass, string propertyName)
        {
            if (propertyName == "Id") propertyName = "ID";
            return new BasicPropertyAccessor().GetGetter(theClass, propertyName);
        }

        public ISetter GetSetter(Type theClass, string propertyName)
        {
            if (propertyName == "Id") propertyName = "ID";
            return new BasicPropertyAccessor().GetSetter(theClass, propertyName);
        }
    }
}
