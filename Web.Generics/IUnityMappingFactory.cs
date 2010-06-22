using System;
using Microsoft.Practices.Unity;

namespace Web.Generics
{
    public interface IUnityMappingFactory
    {
        void GetMappings(IUnityContainer container);
    }
}
