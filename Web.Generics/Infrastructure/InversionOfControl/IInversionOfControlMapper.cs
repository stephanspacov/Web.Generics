using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.ApplicationServices;
using Web.Generics.ApplicationServices.InversionOfControl;

namespace Web.Generics.Infrastructure.InversionOfControl
{
	public interface IInversionOfControlMapper
	{
		void DefineMappings(IInversionOfControlContainer container);
	}
}
