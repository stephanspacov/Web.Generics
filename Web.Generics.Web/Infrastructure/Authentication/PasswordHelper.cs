using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Web.Generics.Infrastructure.Authentication
{
    internal class PasswordHelper
    {
        private static SHA1Managed hasher = new SHA1Managed();

        internal static string ComputeHash(string password)
        {
            byte[] passwordBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(password);
            byte[] passwordHash = hasher.ComputeHash(passwordBytes);
            return Convert.ToBase64String(passwordHash, 0, passwordHash.Length);
        }

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
