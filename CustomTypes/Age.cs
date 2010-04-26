using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Web.Generics.CustomTypes
{
    public class Age
    {
        virtual public Int32 Value { get; set; }
        public Age(Int32 value)
        {
            if (value < 0 || value > 100) throw new ArgumentOutOfRangeException();
        }
    }
}
