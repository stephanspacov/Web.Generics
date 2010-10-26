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
        void SaveOrUpdate(T user);
        T Select(string username, string password);
        T Select(string email);
        bool ChangePassword(string username, string currentPassword, string newPassword);
        bool ChangePassword(string username, string newPassword);
        bool SetValidationKey(string email, string validationKey);
    }
}
