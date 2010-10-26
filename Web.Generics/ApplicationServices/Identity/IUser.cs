using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.ApplicationServices.Identity
{
    public interface IUser
    {
        string Email { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string ValidationKey { get; set; }
    }
}
