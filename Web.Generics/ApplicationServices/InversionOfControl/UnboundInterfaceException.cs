using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.ApplicationServices.InversionOfControl
{
	[global::System.Serializable]
	public class UnboundInterfaceException : Exception
	{
		public UnboundInterfaceException() { }
		public UnboundInterfaceException(string message) : base(message) { }
		public UnboundInterfaceException(string message, Exception inner) : base(message, inner) { }
		protected UnboundInterfaceException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
