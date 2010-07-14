using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web.Generics.Validators
{
    public class EmailAttribute : ValidationAttribute
    {
        private readonly Regex regex = new Regex(@"^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$");

        public EmailAttribute() { }

        public EmailAttribute(String messageResourceName)
        {
            ErrorMessageResourceName = messageResourceName;
        }

        public override bool IsValid(object value)
        {
            String email = Convert.ToString(value);
            if (String.IsNullOrEmpty(email))
            {
                return false;
            }

            Match match = regex.Match(email);
            return match.Success;
        }
    }
}
