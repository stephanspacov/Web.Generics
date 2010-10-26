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
using System.Reflection;
using System.Web.Mvc;
using Web.Generics.WebMvc.UserInterface;

namespace Web.Generics.UserInterface
{
    public class GenericViewModel<T> : GenericViewModel
	{
        public GenericViewModel()
        {
            this.Instance = Activator.CreateInstance<T>();
            //this.InstanceList = Activator.CreateInstance<List<T>>();
            //this.DeletedItems = new List<Int32>();
            //this.SelectLists = new Dictionary<String, SelectList>();
            //this.SelectListValues = new Dictionary<String, Object>();
        }

		virtual new public T Instance { get; set; }

		/*
		virtual public IList<T> InstanceList { get; set; }

        virtual public IDictionary<String, SelectList> SelectLists { get; private set; }
        virtual public IDictionary<String, Object> SelectListValues { get; private set; }

        virtual public IList<Int32> DeletedItems { get; set; }*/

        //public IWebGrid<T> DefaultGrid { get; set; }

/*        public List<PropertyInfo> Properties
        {
            get
            {
                List<PropertyInfo> props = new List<PropertyInfo>();
                PropertyInfo[] propertyArray = this.Instance.GetType().GetProperties();
                foreach (PropertyInfo propertyInfo in propertyArray)
                {
                    props.Add(propertyInfo);
                }
                return props;
            }
        }*/
    }
}
