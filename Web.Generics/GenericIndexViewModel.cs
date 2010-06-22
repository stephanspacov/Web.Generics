using System;
using System.Collections.Generic;
using System.Reflection;

namespace Web.Generics
{
	public class GenericIndexViewModel
	{
		public List<Object> InstanceList { get; set; }
		public List<PropertyInfo> Properties
		{
			get
			{
				List<PropertyInfo> props = new List<PropertyInfo>();
				PropertyInfo[] propertyArray = this.InstanceList.GetType().GetGenericTypeDefinition().GetProperties();
				foreach (PropertyInfo propertyInfo in propertyArray)
				{
					props.Add(propertyInfo);
				}
				return props;
			}
		}
	}
}
