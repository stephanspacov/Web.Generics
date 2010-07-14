using System;
using System.Web.Security;

namespace Web.Generics.ApplicationServices.Authentication
{
    public interface IMembershipRepository
    {
        void CreateUser(MembershipUser user, string hashedPassword);
        MembershipUser SelectUserByID(int id);
        MembershipUser SelectUserByLogin(string username, string hashedPassword);
        MembershipUser SelectUserByName(string username);
        void UpdateUser(MembershipUser user);
        void DeleteUser(string username, bool deleteAllRelatedData);
        void ChangePassword(string user, string hashedNewPassword);
        bool IsEmailUnique(string email);
        bool IsUserNameUnique(string username);
        string GetUserNameByEmail(string email);
        MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords);
        MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);
        MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords);
    }
}
