using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.ApplicationServices.Identity
{
    public interface IUserRepository<T>
    {
        RegisterStatus VerifyUniqueUser(T user);
        void InsertUser(T user);
        T Select(string username, string password);
    }
}
