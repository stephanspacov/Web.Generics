using System;
using System.Web.Security;

namespace Web.Generics.ApplicationServices.Authentication
{
    public interface IMembershipConverter<T>
    {
        MembershipUser ToMembershipUser(T obj);
    }
}
