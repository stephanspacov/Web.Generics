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
            var username = usernameProperty.Invoke(userInstance);
            var email = emailProperty.Invoke(userInstance);

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
