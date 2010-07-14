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
