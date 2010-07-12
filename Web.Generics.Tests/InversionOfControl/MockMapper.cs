using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.Infrastructure.InversionOfControl;
using Web.Generics.ApplicationServices.InversionOfControl;

namespace Web.Generics.Tests.InversionOfControl
{
    public class MockMapper : IInversionOfControlMapper
    {
        public void DefineMappings(IInversionOfControlContainer container)
        {
            container.RegisterType<IMathPower, PowerOfTwo>();
            container.RegisterType<MathMock, MathMock>();
        }
    }
}
