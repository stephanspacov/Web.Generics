using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.Web.Configuration;
using System.Configuration;
using System.Reflection;
using System.Resources;

namespace Web.Generics.Web.Extensions
{
    public static class EnumExtensions
    {
        // Raphael Cruzeiro 2010-08-12
        /// <summary>
        /// Converts the enum to a friendly string. If no friendly string is defined returns the enum's ToString method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enu"></param>
        /// <returns></returns>
        public static string ToString<T>(this Enum enu)
        {
            string key = String.Empty;

            Type type = typeof(T);

            key += type.Name + "_" + enu.ToString();

            WebGenericsSection section = (WebGenericsSection)ConfigurationManager.GetSection("WebGenerics");

            foreach (EnumResourceProperty e in section.EnumResourcesProperties)
            {
                Assembly assembly = Assembly.Load(e.Assembly);

                string[] resourceNames = assembly.GetManifestResourceNames();

                ResourceManager rm = null;

                for (int i = 0; i < resourceNames.Length; i++)
                    if (resourceNames[i].Contains("Enums.resources"))
                    {
                        rm = new ResourceManager(resourceNames[i].Substring(0, resourceNames[i].Length - 10), assembly);

                        return rm.GetString(key);
                    }
            }

            return enu.ToString();
        }
    }
}
