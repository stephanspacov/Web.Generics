using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.CustomTypes
{
    public class BrazilianPhoneNumber : PhoneNumber
    {
        public BrazilianPhoneNumber(String value) : base(value)
        {
            base.ValidFormat = @"\(\d{2}\) \d{4}\-\d{4}";
        }
    }
}
