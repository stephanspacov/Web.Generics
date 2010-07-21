using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Web.Generics.CustomTypes
{
    public class PhoneNumber
    {
        public String AreaCode { get; private set; }
        public String NumberWithoutAreaCode { get; private set; }

		public Cpf Cpf { get; set; }

        public String Value { get; private set; }
        protected String ValidFormat;

        public PhoneNumber(String value)
        {
            //if (!new Regex(ValidFormat).IsMatch(value)) throw new ArgumentOutOfRangeException();
            this.Value = value;
        }
    }
}
