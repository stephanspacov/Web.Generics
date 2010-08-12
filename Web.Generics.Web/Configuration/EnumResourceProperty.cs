using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Web.Generics.Web.Configuration
{
    // Raphael Cruzeiro 2010-08-12
    /// <summary>
    /// Represents a configuration element that specifies an Assembly that contains the resources for the enum's friendly names
    /// </summary>
    public class EnumResourceProperty : ConfigurationElement
    {
        [ConfigurationProperty("assembly", IsRequired = true)]
        public string Assembly
        {
            get { return this["assembly"] as string; }
        }
    }
}
