using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.ApplicationServices;

namespace Web.Generics.Infrastructure.InversionOfControl
{
	public interface IInversionOfControlMapper
	{
		void DefineMappings(IInversionOfControlContainer container);
	}
}
