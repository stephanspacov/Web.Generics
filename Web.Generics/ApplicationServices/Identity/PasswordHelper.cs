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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Web.Generics.ApplicationServices.Identity
{
    /// <summary>
    /// A helper class for handling passwords
    /// </summary>
    internal class PasswordHelper
    {
        private static SHA1Managed hasher = new SHA1Managed();

        /// <summary>
        /// Computes a SHA-1 hash for the given string
        /// </summary>
        /// <param name="password">The string to be hashesd</param>
        /// <returns>The hashed string</returns>
        internal static string ComputeHash(string password)
        {
            byte[] passwordBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(password);
            byte[] passwordHash = hasher.ComputeHash(passwordBytes);
            return Convert.ToBase64String(passwordHash, 0, passwordHash.Length);
        }

        /// <summary>
        /// Generates a random alphanumeric password
        /// </summary>
        /// <returns>The generated password</returns>
        internal static string Generate()
        {

            string result = String.Empty;
            char[] letters = "abcdefghijklmnopqrstvxyzwABCDEFGHIJKLMNOPQRSTUVXYZW".ToCharArray();
            Random random = new Random(DateTime.Now.Second);
            for (int i = 0; i < 8; i++)
            {
                if (i > 5)
                {
                    result += random.Next(0, 9);
                    continue;
                }
                int index = random.Next(0, letters.Length);
                result += letters[index];
            }

            return result;

        }
    }
}
