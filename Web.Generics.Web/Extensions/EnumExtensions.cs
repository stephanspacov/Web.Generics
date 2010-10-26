/*
Copyright 2010 Inspira Tecnologia.
All Rights Reserved.

Contact: Thiago Alves <thiago.alves@inspira.com.br>

This file is part of Web.Generics

Web.Generics is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Web.Generics is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with Web.Generics.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.Web.Configuration;
using System.Configuration;
using System.Reflection;
using System.Resources;

namespace Web.Generics.UserInterface.Extensions
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
