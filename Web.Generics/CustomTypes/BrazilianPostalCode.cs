using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.CustomTypes
{
    public class BrazilianPostalCode : PostalCode
    {
        public BrazilianPostalCode(String value) : base(value)
        {
            base.ValidFormat = @"\d{5}-\d{3}";
        }
    }
}
