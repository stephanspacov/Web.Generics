using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.Tests.InversionOfControl
{
    public class PowerOfTwo : IMathPower
    {
        public int ElevateTo(int number)
        {
            return number * number;
        }
    }
}
