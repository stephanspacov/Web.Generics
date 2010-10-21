using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.UserInterface.Models
{
	public class GridCell
	{
		public Object Value { get; set; }
		public String Text
		{
			get
			{
				if (Value == null) return "";
				return Value.ToString();
			}
		}
	}
}
