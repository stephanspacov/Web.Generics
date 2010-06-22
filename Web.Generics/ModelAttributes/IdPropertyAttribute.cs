using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.ModelAttributes
{
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class IdPropertyAttribute : Attribute
    {
        readonly string propertyName;

        public IdPropertyAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public string PropertyName
        {
            get { return this.propertyName; }
        }
    }
}
