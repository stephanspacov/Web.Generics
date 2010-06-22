using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Web.Generics.CustomTypes
{
    public class EmailAddress
    {
        virtual public String Value { get; set; }
        public EmailAddress(String value)
        {
            this.Value = value;
        }
    }
}
