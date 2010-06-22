using System;
using System.Web.Security;

namespace Web.Generics
{
    public interface IMembershipConverter<T>
    {
        MembershipUser ToMembershipUser(T obj);
    }
}
