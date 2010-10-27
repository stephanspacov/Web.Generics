/*
Copyright 2010 Inspira Tecnologia.
All Rights Reserved.

Contact: Thiago Alves <thiago.alves@inspira.com.br>

This file is part of Web.Generics

Web.Generics is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Web.Generics is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with Web.Generics.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Web.Generics.ApplicationServices.Identity
{
    /// <summary>
    /// Handles user authentication
    /// </summary>
    /// <typeparam name="T">The user entity</typeparam>
    public class IdentityService<T>
    {
        private readonly IUserRepository<T> userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository">The repository responsible for persisting the users</param>
        public IdentityService(IUserRepository<T> userRepository)
        {
            this.userRepository = userRepository;
        }
        
        [Obsolete]
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

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="userInstance">The user object</param>
        /// <param name="usernameProperty">The property that represents the username</param>
        /// <param name="emailProperty">The property that represents the user's email</param>
        /// <param name="encryptedPasswordProperty">The property that represents the user's password</param>
        /// <param name="cleanPassword">The clear text password to be hashed and stored in the user object</param>
        /// <returns>The status of the register operation</returns>
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

        /// <summary>
        /// Checks if a given string is an email
        /// </summary>
        /// <param name="email">The string to be validated</param>
        /// <returns>True if the string is an email</returns>
        private bool IsValidEmail(string email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
                return (true);
            else
                return (false);
        }

        /// <summary>
        /// Hashes the password using a SHA-1 algorithm
        /// </summary>
        /// <param name="password">The clear text password</param>
        /// <returns>The hashed password</returns>
        public string EncryptPassword(string password)
        {
            return PasswordHelper.ComputeHash(password);
        }

        /// <summary>
        /// Validates the authencity of the credentials supplied by the user
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>True if the user's credentials validate</returns>
        public bool Validate(string username, string password)
        {
            string hashedPassword = this.EncryptPassword(password);

            return this.userRepository.Select(username, hashedPassword) != null;
        }

        /// <summary>
        /// Changes the user's password validating it's current password
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="currentPassword">The user's current password</param>
        /// <param name="newPassword">The new password</param>
        /// <returns>The status of the change password operation</returns>
        public PasswordChangeStatus ChangePassword(string username, string currentPassword, string newPassword)
        {
            string hashedNewPassword = this.EncryptPassword(newPassword);
            string hashedCurrentPassword = this.EncryptPassword(currentPassword);

            return this.userRepository.ChangePassword(username, hashedCurrentPassword, hashedNewPassword) ? PasswordChangeStatus.Success : PasswordChangeStatus.InvalidCurrentPassword;
        }

        /// <summary>
        /// Changes the user's password disregarding it's current password
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="newPassword">The new password</param>
        /// <returns>The status of the change password operation</returns>
        public PasswordChangeStatus AdministrativePasswordChange(string username, string newPassword)
        {
            string hashedNewPassword = this.EncryptPassword(newPassword);

            return this.userRepository.ChangePassword(username, hashedNewPassword) ? PasswordChangeStatus.Success : PasswordChangeStatus.InexistentUser;
        }

        /// <summary>
        /// Generates a validation key to be sent to the user to confirm her identity in case of a forgotten password
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <returns>The generated validation key or null if there is no user registered with the provided email</returns>
        public string GenerateValidationKey(string email)
        {
            string validationKey = Guid.NewGuid().ToString();

            //The validation key cannot be stored in plain text
            string hashedValidationKey = this.EncryptPassword(validationKey);

            if (!userRepository.SetValidationKey(email, hashedValidationKey))
                return null;

            return validationKey;
        }

        /// <summary>
        /// Generates a new, random password for the supplied user
        /// </summary>
        /// <param name="username">The username</param>
        /// <returns>The new password or null if there is no user registered with the provided username</returns>
        public string ResetPassword(string username)
        {
            string hashedNewPassword = PasswordHelper.ComputeHash(PasswordHelper.Generate());

            return userRepository.ChangePassword(username, hashedNewPassword) ? hashedNewPassword : null;
        }

        /// <summary>
        /// Generates a new, random password for the supplied user after validating it's validation key
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="validationKey">The user's validation key</param>
        /// <returns>The new password or null if there's no user registered with the supplied username or if the validation key does not match the previous validation key generated by the system</returns>
        public string ResetPasswordWithValidationKey(string username, string validationKey)
        {
            string hashedNewPassword = PasswordHelper.ComputeHash(PasswordHelper.Generate());

            return userRepository.ChangePasswordWithValidationKey(username, PasswordHelper.ComputeHash(validationKey), hashedNewPassword) ? hashedNewPassword : null;
        }
    }
}
