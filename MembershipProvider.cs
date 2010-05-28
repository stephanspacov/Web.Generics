using System;
using System.Web.Security;
using System.Reflection;
using System.Configuration.Provider;

namespace Web.Generics
{
    public class MembershipProvider : System.Web.Security.MembershipProvider
    {
        private static IMembershipRepository repository;

        public IMembershipRepository Repository
        {
            get
            {
                if (repository == null)
                    repository = this.GetRepository();
                return repository;
            }
        }

        private IMembershipRepository GetRepository()
        {
            string assembly = String.Empty;
            string repositoryType = String.Empty;

            System.Configuration.Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            System.Web.Configuration.MembershipSection membershipSection = (System.Web.Configuration.MembershipSection)configuration.GetSection("system.web/membership");

            foreach (System.Configuration.ProviderSettings p in membershipSection.Providers)
            {
                if (p.Name == membershipSection.DefaultProvider)
                {
                    assembly = p.Parameters["repositoryAssembly"];
                    repositoryType = p.Parameters["repository"];
                }
            }

            Assembly thisAssembly = Assembly.GetAssembly(typeof(MembershipProvider));
            string currentAssembly = thisAssembly.FullName;

            try
            {
                if (assembly != currentAssembly)
                    thisAssembly = Assembly.Load(assembly);
            }
            catch (Exception e)
            {
                throw new ProviderException("Inspira MembershipProvider: Error loading the repository's assembly.");
            }

            try
            {
                Type repType = thisAssembly.GetType(repositoryType);
                return (IMembershipRepository)Activator.CreateInstance(repType, true);
            }
            catch (Exception e)
            {
                throw new ProviderException("Error creating instance of the repository.");
            }
        }

        public override string ApplicationName
        {
            get
            {
                System.Configuration.Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                System.Web.Configuration.MembershipSection membershipSection = (System.Web.Configuration.MembershipSection)configuration.GetSection("system.web/membership");

                foreach (System.Configuration.ProviderSettings p in membershipSection.Providers)
                    if (p.Name == membershipSection.DefaultProvider)
                        return p.Parameters["applicationName"];

                return String.Empty;
            }
            set { }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            string hashedOldPassword = PasswordHelper.ComputeHash(oldPassword);
            string hashedNewPassword = PasswordHelper.ComputeHash(newPassword);
            if (Repository.SelectUserByLogin(username, hashedOldPassword) != null) 
            {
                Repository.ChangePassword(username, hashedNewPassword);
                return true;
            }
            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            if (!Repository.IsUserNameUnique(username))
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }

            if (!Repository.IsEmailUnique(email)) 
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            string hashedPassword = PasswordHelper.ComputeHash(password);
            Repository.CreateUser
                (new MembershipUser(
                Membership.Provider.Name,
                username,
                null,
                email,
                passwordQuestion,
                string.Empty,
                isApproved,
                false,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now,
                DateTime.Now
                ),
                hashedPassword
                );

            MembershipUser user = Repository.SelectUserByLogin(username, hashedPassword);

            if (user != null)
            {
                if (user.UserName == username)
                    status = MembershipCreateStatus.Success;
                else
                    status = MembershipCreateStatus.UserRejected;
            }
            else
                status = MembershipCreateStatus.UserRejected;

            return user;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            Repository.DeleteUser(username, deleteAllRelatedData);
            return true;
        }

        public override bool EnablePasswordReset
        {
            get
            {
                System.Configuration.Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                System.Web.Configuration.MembershipSection membershipSection = (System.Web.Configuration.MembershipSection)configuration.GetSection("system.web/membership");

                foreach (System.Configuration.ProviderSettings p in membershipSection.Providers)
                    if (p.Name == membershipSection.DefaultProvider)
                        return Convert.ToBoolean(p.Parameters["enablePasswordReset"]);

                return false;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                System.Configuration.Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                System.Web.Configuration.MembershipSection membershipSection = (System.Web.Configuration.MembershipSection)configuration.GetSection("system.web/membership");

                foreach (System.Configuration.ProviderSettings p in membershipSection.Providers)
                    if (p.Name == membershipSection.DefaultProvider)
                        return Convert.ToBoolean(p.Parameters["enablePasswordRetrieval"]);

                return false;
            }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return Repository.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return Repository.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            return Repository.GetAllUsers(pageIndex, pageSize, out totalRecords);
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return Repository.SelectUserByName(username);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return Repository.SelectUserByID((int)providerUserKey);
        }

        public override string GetUserNameByEmail(string email)
        {
            return Repository.GetUserNameByEmail(email);
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                System.Configuration.Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                System.Web.Configuration.MembershipSection membershipSection = (System.Web.Configuration.MembershipSection)configuration.GetSection("system.web/membership");

                foreach (System.Configuration.ProviderSettings p in membershipSection.Providers)
                    if (p.Name == membershipSection.DefaultProvider)
                        return Convert.ToInt32(p.Parameters["maxInvalidPasswordAttempts"]);

                return -1;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                System.Configuration.Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                System.Web.Configuration.MembershipSection membershipSection = (System.Web.Configuration.MembershipSection)configuration.GetSection("system.web/membership");

                foreach (System.Configuration.ProviderSettings p in membershipSection.Providers)
                    if (p.Name == membershipSection.DefaultProvider)
                        return Convert.ToInt32(p.Parameters["minRequiredNonalphanumericCharacters"]);

                return -1;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                System.Configuration.Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                System.Web.Configuration.MembershipSection membershipSection = (System.Web.Configuration.MembershipSection)configuration.GetSection("system.web/membership");

                foreach (System.Configuration.ProviderSettings p in membershipSection.Providers)
                    if (p.Name == membershipSection.DefaultProvider)
                        return Convert.ToInt32(p.Parameters["minRequiredPasswordLength"]);

                return -1;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                System.Configuration.Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                System.Web.Configuration.MembershipSection membershipSection = (System.Web.Configuration.MembershipSection)configuration.GetSection("system.web/membership");

                foreach (System.Configuration.ProviderSettings p in membershipSection.Providers)
                    if (p.Name == membershipSection.DefaultProvider)
                        return Convert.ToInt32(p.Parameters["passwordAttemptWindow"]);

                return -1;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            string pass = PasswordHelper.Generate();
            string newPass = PasswordHelper.ComputeHash(pass);
            repository.ChangePassword(username, newPass);
            return pass;
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            if (Repository.SelectUserByLogin(username, PasswordHelper.ComputeHash(password)) != null)
                return true;
            else
                return false;
        }
    }
}
