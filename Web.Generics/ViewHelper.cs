using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Web.Generics.ModelAttributes;

namespace Web.Generics
{
	public class ViewHelper
	{
		public static PropertyInfo[] GetProperties(Object obj)
		{
			if (obj == null) return new PropertyInfo[0];
			Type objectType = obj.GetType();
			if (objectType.IsGenericType)
			{
				return obj.GetType().GetProperty("Item").PropertyType.GetProperties();
			}
			return obj.GetType().GetProperties();
		}

		public static Object GetParamObject(Object obj)
        {
            IdPropertyAttribute idProperty = (IdPropertyAttribute)obj.GetType().GetCustomAttributes(typeof(IdPropertyAttribute), true).SingleOrDefault();
            string idPropertyName = idProperty != null ? idProperty.PropertyName : "ID";
            return new { id = obj.GetType().GetProperty(idPropertyName).GetValue(obj, null) };
		}
	}
}
