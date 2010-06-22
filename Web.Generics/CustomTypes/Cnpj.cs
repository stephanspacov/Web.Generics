using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Web.Generics.CustomTypes
{
    public class Cnpj
    {
        virtual public String Value { get; set; }
        public Cnpj(String value)
        {
            if (String.IsNullOrEmpty(value)) throw new ArgumentNullException();
            value = new Regex("[^0-9]").Replace(value, "");
            if (value.Length != 14) throw new ArgumentOutOfRangeException();
            this.Value = Value;
        }
    }
}
