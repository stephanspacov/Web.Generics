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
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
                return (true);
            else
                return (false);
        }

        public string EncryptPassword(string password)
        {
            return PasswordHelper.ComputeHash(password);
        }

        public bool Validate(string username, string password)
        {
            string hashedPassword = this.EncryptPassword(password);

            return this.userRepository.Select(username, hashedPassword) != null;
        }

        public PasswordChangeStatus ChangePassword(string username, string currentPassword, string newPassword)
        {
            string hashedNewPassword = this.EncryptPassword(newPassword);
            string hashedCurrentPassword = this.EncryptPassword(currentPassword);

            return this.userRepository.ChangePassword(username, hashedCurrentPassword, hashedNewPassword) ? PasswordChangeStatus.Success : PasswordChangeStatus.InvalidCurrentPassword;
        }

        public PasswordChangeStatus AdministrativePasswordChange(string username, string newPassword)
        {
            string hashedNewPassword = this.EncryptPassword(newPassword);

            return this.userRepository.ChangePassword(username, hashedNewPassword) ? PasswordChangeStatus.Success : PasswordChangeStatus.UnexistentUser;
        }

        public string GenerateValidationKey(string email)
        {
            string validationKey = Guid.NewGuid().ToString();

            //The validation key cannot be stored in plain text
            string hashedValidationKey = this.EncryptPassword(validationKey);

            if (!userRepository.SetValidationKey(email, hashedValidationKey))
                return null;

            return validationKey;
        }
    }
}
