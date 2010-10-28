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

namespace Web.Generics.ApplicationServices.Identity
{
    /// <summary>
    /// A generic repository interface for persisting the application's users
    /// </summary>
    /// <typeparam name="T">The type of the user object</typeparam>
    public interface IUserRepository<T>
    {
        /// <summary>
        /// Verifies if the supplied user is unique within the system
        /// </summary>
        /// <param name="user">The user to be verified for uniqueness</param>
        /// <returns>The resulting status of the cerification</returns>
        RegisterStatus VerifyUniqueUser(T user);
        /// <summary>
        /// Persists a new user
        /// </summary>
        /// <param name="user">The user to be persisted</param>
        void InsertUser(T user);

        /// <summary>
        /// Saves or updates the supplied user
        /// </summary>
        /// <param name="user">The user to be saved or updated</param>
        void SaveOrUpdate(T user);

        /// <summary>
        /// Selects an users based on the supplied login information
        /// </summary>
        /// <param name="username">The suername</param>
        /// <param name="password">the password</param>
        /// <returns>The user or null if no user is found based on the supplied login</returns>
        T Select(string username, string password);
        /// <summary>
        /// Selects an user based on the supplied email
        /// </summary>
        /// <param name="email">The email</param>
        /// <returns>The user or null if no user is found based on the supplied email</returns>
        T Select(string email);

        /// <summary>
        /// Selects an user based on the supplied login information and changes it's password
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="currentPassword">The current password</param>
        /// <param name="newPassword">The new password</param>
        /// <returns>True if a user was found and it's password was changed</returns>
        bool ChangePassword(string username, string currentPassword, string newPassword);

        /// <summary>
        /// Selects an user based on the supplied username and changes it's password
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="newPassword">The new password</param>
        /// <returns>True if a user was found and it's password was changed</returns>
        bool ChangePassword(string username, string newPassword);

        /// <summary>
        /// Selects an user based on the supplied username and validation key and changes it's password
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="validationKey">The validation key</param>
        /// <param name="newPassword">The new password</param>
        /// <returns>True if a user was found and it's password was changed</returns>
        bool ChangePasswordWithValidationKey(string username, string validationKey, string newPassword);

        /// <summary>
        /// Persists a validation key generated for an user
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <param name="validationKey">The validation key</param>
        /// <returns>True if the user was found and it's validation key persisted</returns>
        bool SetValidationKey(string email, string validationKey);
    }
}
