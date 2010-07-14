using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.ApplicationServices.InversionOfControl
{
	[global::System.Serializable]
	public class DuplicateTypeException : Exception
	{
		public DuplicateTypeException() { }
		public DuplicateTypeException(string message) : base(message) { }
		public DuplicateTypeException(string message, Exception inner) : base(message, inner) { }
		protected DuplicateTypeException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
