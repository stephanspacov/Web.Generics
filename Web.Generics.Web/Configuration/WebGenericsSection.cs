using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Web.Generics.Web.Configuration
{
    // Raphael Cruzeiro 2010-08-12
    /// <summary>
    /// Defines configuration settings for Web.Generics
    /// </summary>
    public class WebGenericsSection : ConfigurationSection
    {
        public static WebGenericsSection GetConfig()
        {
            return (WebGenericsSection)ConfigurationManager.GetSection("WebGenerics");
        }
        /// <summary>
        /// Gets a EnumResourcesPropertyCollection
        /// </summary>
        [ConfigurationProperty("EnumResources", IsRequired = false)]
        public EnumResourcesPropertyCollection EnumResourcesProperties
        {
            get
            {
                return this["EnumResources"] as EnumResourcesPropertyCollection;
            }
        }
    }
}
