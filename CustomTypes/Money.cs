using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Web.Generics.CustomTypes
{
    public class Money
    {
        virtual public Decimal Value { get; set; }
        public Money(Decimal value)
        {
            this.Value = value;
        }
    }
}
