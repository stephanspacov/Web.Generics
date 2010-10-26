using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Web.Generics.ApplicationServices.Identity
{
    public class IdentityService<T>
    {
        private readonly IUserRepository<T> userRepository;
        public IdentityService(IUserRepository<T> userRepository)
        {
            this.userRepository = userRepository;
        }

        public RegisterStatus Register(string username, string password, string email, Func<String, String, RegisterStatus> userExists, Action insertUser)
        {

            if (username == null)
                throw new ArgumentNullException("username");
            if (password == null)
                throw new ArgumentNullException("password");
            if (email== null)
                throw new ArgumentNullException("email");
            if (userExists == null)
                throw new ArgumentNullException("userExists");
            if (password == null)
                throw new ArgumentNullException("insertUser");

            if (IsValidEmail(email)) return RegisterStatus.InvalidEmail;

            RegisterStatus status = userExists(username, email);
            if (status == RegisterStatus.EmailAlreadyExists || status == RegisterStatus.UsernameAlreadyExists)
            {
                return status;
            }
            insertUser.Invoke();
            return RegisterStatus.Success;
        }

        public RegisterStatus Register(T userInstance, Func<T, String> usernameProperty, Func<T, String> emailProperty, Action<String> encryptedPasswordProperty, String cleanPassword)
        {
            if (userInstance == null)
                throw new ArgumentNullException("userInstance");
            if (encryptedPasswordProperty == null)
                throw new ArgumentNullException("encryptedPasswordProperty");
            if (emailProperty == null)
                throw new ArgumentNullException("emailProperty");
            if (cleanPassword == null)
                throw new ArgumentNullException("cleanPassword");




            var username = usernameProperty.Invoke(userInstance);
            var email = emailProperty.Invoke(userInstance);


            if (username == null)
                throw new ArgumentNullException("insertUser");
            if (email == null)
                throw new ArgumentNullException("insertUser");


            if (!IsValidEmail(email)) return RegisterStatus.InvalidEmail;
            // TODO: verificar username e senha

            RegisterStatus status = this.userRepository.VerifyUniqueUser(userInstance);
            if (status == RegisterStatus.EmailAlreadyExists || status == RegisterStatus.UsernameAlreadyExists)
            {
                return status;
            }

            // Ready to insert user
            var encryptedPassword = this.EncryptPassword(cleanPassword);
            encryptedPasswordProperty.Invoke(encryptedPassword);

            this.userRepository.InsertUser(userInstance);
            return RegisterStatus.Success;
        }

        private bool IsValidEmail(string email)
        {
            return true;
        }

        public string EncryptPassword(string password)
        {
            String encryptedPassword = password.GetHashCode().ToString();
            return encryptedPassword;
        }
    }
}
