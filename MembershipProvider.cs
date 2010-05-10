using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
namespace Inspira.Membership.MembershipProvider
{
    public class MembershipProvider : System.Web.Security.MembershipProvider
    {

        private IMembershipRepository repository;

        public MembershipProvider(IMembershipRepository repository)
        {
            this.repository = repository;
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            string hashedOldPassword = PasswordHelper.ComputeHash(oldPassword);
            string hashedNewPassword = PasswordHelper.ComputeHash(newPassword);
            if (repository.SelectUserByLogin(username, hashedOldPassword) != null) 
            {
                repository.ChangePassword(username, hashedNewPassword);
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

            if (!repository.IsUserNameUnique(username))
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }

            if (!repository.IsEmailUnique(email)) 
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            string hashedPassword = PasswordHelper.ComputeHash(password);
            repository.CreateUser
                (new MembershipUser(
                this.ToString(),
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

            MembershipUser user = repository.SelectUserByLogin(username, hashedPassword);

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
            repository.DeleteUser(username, deleteAllRelatedData);
            return true;
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return repository.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return repository.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            return repository.GetAllUsers(pageIndex, pageSize, out totalRecords);
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
            return repository.SelectUserByName(username);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return repository.SelectUserByID((int)providerUserKey);
        }

        public override string GetUserNameByEmail(string email)
        {
            return repository.GetUserNameByEmail(email);
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
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
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
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
            if (repository.SelectUserByLogin(username, PasswordHelper.ComputeHash(password)) != null)
                return true;
            else
                return false;
        }
    }
}
