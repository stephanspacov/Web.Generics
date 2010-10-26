using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.ApplicationServices.Identity;
using Inspira.Blog.RepositoryInterfaces;
using NHibernate.Criterion;
using NHibernate.Linq;
using Inspira.Blog.DomainModel;

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
            //var query = this.session.CreateQuery("from u in User where u.Username=:username or u.Email=:email select u");
            var query = this.session.Query<User>().Where(u => u.Username == user.Username || u.Email == user.Email);
            var userFromDb = query.SingleOrDefault();

            if (userFromDb == null) return RegisterStatus.Success;
            if (userFromDb.Username == user.Username) return RegisterStatus.UsernameAlreadyExists;
            if (userFromDb.Email == user.Email) return RegisterStatus.EmailAlreadyExists;
            return RegisterStatus.Success;
        }

        public void InsertUser(DomainModel.User user)
        {
            this.session.Save(user);
        }

        public User Select(string username, string password)
        {
            return this.session.Query<User>().Where(x => x.Username == username && x.Password == password).SingleOrDefault();
        }

        public bool ChangePassword(string username, string currentPassword, string newPassword)
        {
            User user = this.session.Query<User>().Where(x => x.Username == username && x.Password == currentPassword).SingleOrDefault();

            if (user != null) 
            {
                user.Password = newPassword;
                this.SaveOrUpdate(user);

                return true;
            }

            return false;
        }

        public User Select(string email)
        {
            return this.session.Query<User>().Where(x => x.Email == email).SingleOrDefault();
        }

        public bool ChangePassword(string username, string newPassword)
        {
            User user = this.session.Query<User>().Where(x => x.Username == username).SingleOrDefault();

            if (user != null)
            {
                user.Password = newPassword;
                this.SaveOrUpdate(user);

                return true;
            }

            return false;
        }

        public bool SetValidationKey(string email, string validationKey)
        {
            User user = this.session.Query<User>().Where(x => x.Email == email).SingleOrDefault();

            if (user == null)
                return false;

            user.ValidationKey = validationKey;

            this.SaveOrUpdate(user);

            return true;
        }
    }
}
