using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Web.Generics.CustomTypes
{
    public abstract class PostalCode
    {
        public String Value { get; set; }
        protected String ValidFormat;

        public PostalCode(String value)
        {
            if (!new Regex(ValidFormat).IsMatch(value)) throw new ArgumentOutOfRangeException();
            this.Value = value;
        }
    }
}
