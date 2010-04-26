using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Web.Generics
{
    public interface IUnityMappingFactory
    {
        void GetMappings(IUnityContainer container);
    }
}
