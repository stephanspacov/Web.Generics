using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Criterion;
using Web.Generics.ApplicationServices.Identity;
using Inspira.Blog.DomainModel;
using Inspira.Blog.Infrastructure.DataAccess.Repositories;

namespace Web.Generics.Tests.Authentication
{
    [TestClass]
    public class MembershipServicesTests
    {
        public MembershipServicesTests() {
            ContextFactory.InitializeAppManager();
        }

        ISession session;
        UserRepository userRepository;
        IdentityService<User> identityService;

        [TestInitialize]
        public void Initialize() {
            session = ApplicationManager.SessionFactory.OpenSession();
            session.BeginTransaction();
            userRepository = new UserRepository(session);
            identityService = new IdentityService<User>(userRepository);
        }


        /* Register: */
        [TestMethod]
        public void Register_with_valid_data_inserts_user_into_database_and_returns_them()
        {
            var password = "neoistheone";
            var user = new User
            {
                Name = "John Doe",
                BirthDate = new DateTime(1982, 8, 1),
                Email = "john.doe@inspira.com.br",
                Username = "john_doe",
                Address = new Address { City = "São paulo", StreetName="name", Number="123B", State="SP", ZipCode="03423-234" }
            };

            try
            {
                identityService.Register(user, u => u.Username, u => u.Email, (s) => user.Password = s, password);
            }
            catch(Exception e)
            {
                session.Transaction.Rollback();
                throw;
            }
        }

        [TestMethod]
        public void Register_with_existing_username_returns_ExistingUsername_state()
        {
            var user = new User
            {
                Name = "John Doe",
                BirthDate = new DateTime(1982, 8, 1),
                Email = "john.doe@inspira.com.br",
                Password = "****",
                Username = "john_doe",
                Address = new Address { City = "São paulo", StreetName = "name", Number = "123B", State = "SP", ZipCode = "03423-234" }
            };

            userRepository.SaveOrUpdate(user);
            var user2 = new User
            {
                Name = "Fulano",
                BirthDate = DateTime.Now,
                Email = "fulano@inspira.com.br",
                Password = "$$$$",
                Username = "john_doe",
                Address = new Address { City = "São paulo", StreetName = "name", Number = "123B", State = "SP", ZipCode = "03423-234" }
            };
            userRepository.SaveOrUpdate(user);
        }

        // Register with existing e-mail returns ExistingEmail state
        // Register with invalid data returns invalid state
        // Register with null data throws argument invalid exception
        // Register with null role throws argumentNullException
        // Register fail must rollback transaction
  
         /* Validate:
         *   - Validate existing user with correct password returns true
         *   - Validate existing user with incorrect password returns false
         *   - Validate non-existing user returns false
         * ChangePassword
         *    - User changing password with correct current password and valid new password returns Success
         *    - Admin changing password with valid new password returns Success
         *    - Admin changing password with invalid new password returns InvalidNewPassword
         *    - User changing password with incorrect current password returns IncorrectNewPassword
         *    - User changing password with correct current password and invalid new password returns InvalidNewPassword
         * ResetPassword:
         *    - Password reset for existing user changes password and returns it
         *    - Password reset for non-existing user returns null
         * ResetPasswordWithValidationKey:
         *    - Password reset for existing user with valid validation key changes password and returns it
         *    - Password reset for existing user with invalid validation key returns null
         *    - Password reset for non-existing user returns null
         * GenerateValidationKey:
         *    - GenerateValidationKey for existing email generates key and returns it
         *    - GenerateValidationKey for nonexisting email returns null
         * Unlock:
         *     - Unlock verify if the user exists and update lock flag to false
         * Lock:
         *     - Unlock verify if the user exists and update lock flag to true
         * 
         */

        /*
        [TestMethod]
        [Ignore]
        public void OLD___Register_with_valid_data_inserts_user_into_database_and_returns_them()
        {
            IUserRepository userRepository = new AppUserRepository(session);
            IdentityService service = new IdentityService<User>();

            RegisterStatus status = RegisterStatus.Success;

            IUser user = null; /*service.Register(
                "John.Doe",
                "*********",
                "john.doe@test.com",
                out status
                );

            IUser persistedUser = (IUser)session.CreateCriteria<IUser>().Add(Restrictions.Eq("Username", user.Username)).List()[0];

            Assert.AreEqual(RegisterStatus.Success, status);
            Assert.IsNotNull(user);
            Assert.AreEqual(persistedUser.Email, user.Email);
        }
        */

        [TestCleanup]
        public void Cleanup()
        {
            try
            {
                session.Transaction.Commit();
            }
            catch
            {
                session.Transaction.Rollback();
                throw;
            }
            session.Dispose();  
            ApplicationManager.SessionFactory.Dispose();
        }
    }
}
