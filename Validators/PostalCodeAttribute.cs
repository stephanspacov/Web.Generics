using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web.Generics.Validators
{
    public class PostalCodeAttribute : ValidationAttribute
    {
        private readonly Regex regex = new Regex(@"\d{5}-\d{3}");
        public PostalCodeAttribute() { }

        public PostalCodeAttribute(String messageResourceName)
        {
            ErrorMessageResourceName = messageResourceName;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            String code = Convert.ToString(value);
            if (!regex.Match(code).Success)
            {
                return false;
            }

            return true;
        }
    }
}
