using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.ApplicationServices.Identity;
using Inspira.Blog.RepositoryInterfaces;
using NHibernate.Criterion;

namespace Inspira.Blog.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private NHibernate.ISession session;

        public UserRepository(NHibernate.ISession session)
        {
            this.session = session;
        }

        public void SaveOrUpdate(DomainModel.User user)
        {
            this.session.SaveOrUpdate(user);            
        }

        public void ChangeBirthDate(int id, DateTime newBirthDate)
        {
            throw new NotImplementedException();
        }

        public RegisterStatus VerifyUniqueUser(DomainModel.User user)
        {
            var query = this.session.CreateQuery("from u in User where u.Username=:username or u.Email=:email select u");
            query.SetParameter("username", user.Username);
            query.SetParameter("email", user.Email);
            var userFromDb = query.UniqueResult<DomainModel.User>();

            if (userFromDb == null) return RegisterStatus.Success;
            if (userFromDb.Username == user.Username) return RegisterStatus.UsernameAlreadyExists;
            if (userFromDb.Email == user.Email) return RegisterStatus.EmailAlreadyExists;
            return RegisterStatus.Success;
        }

        public void InsertUser(DomainModel.User user)
        {
            this.session.Save(user);
        }
    }
}
