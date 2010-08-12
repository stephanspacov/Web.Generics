using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Web.Generics.Web.Configuration
{
    // Raphael Cruzeiro 2010-08-12
    /// <summary>
    /// A collection of EnumResourcesProperty
    /// </summary>
    public class EnumResourcesPropertyCollection : ConfigurationElementCollection
    {
        public EnumResourceProperty this[int index]
        {
            get { return base.BaseGet(index) as EnumResourceProperty; }

            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new EnumResourceProperty();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EnumResourceProperty)element).Assembly;
        }
    }
}
