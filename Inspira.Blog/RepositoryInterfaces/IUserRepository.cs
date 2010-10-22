using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.Identity;

namespace Inspira.Blog.RepositoryInterfaces
{
    public interface IUserRepository : IUserRepository<User>
    {
        void ChangeBirthDate(int id, DateTime newBirthDate);
    }
}
