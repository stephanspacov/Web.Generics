using System;
using System.Collections.Generic;
using System.Reflection;

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
			return new { id =  obj.GetType().GetProperty("ID").GetValue(obj, null) };
		}
	}
}
