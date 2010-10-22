using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.ApplicationServices.Identity
{
    public enum RegisterStatus
    {
        Success,
        EmailAlreadyExists,
        UsernameAlreadyExists,
        InvalidEmail
    }
}
