using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web.Generics.Validators
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        private readonly Regex regex = new Regex(@"(^\(\d{2}\)[ ]\d{4}-\d{4}|(\d{2}[ ]\d{4}-\d{4})|(\d{2}[ ]\d{8})|(\d{10})$)");
        public PhoneNumberAttribute() { }

        public PhoneNumberAttribute(String messageResourceName)
        {
            ErrorMessageResourceName = messageResourceName;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            String telefone = Convert.ToString(value);
            if (!regex.Match(telefone).Success)
            {
                return false;
            }

            return true;
        }
    }
}
