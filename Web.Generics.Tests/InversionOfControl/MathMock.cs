using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.Tests.InversionOfControl
{
    public class MathMock
    {
        IMathPower power;
        public MathMock(IMathPower power) {
            this.power = power;
        }

        public Int32 Power(Int32 number)
        {
            return power.ElevateTo(number);
        }
    }
}
