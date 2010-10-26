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
