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
