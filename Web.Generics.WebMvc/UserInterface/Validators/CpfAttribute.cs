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
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web.Generics.Validators
{
    public class CpfAttribute : ValidationAttribute
    {
        private readonly Regex regex = new Regex(@"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)");
        public CpfAttribute() { }

        public CpfAttribute(String messageResourceName)
        {
            ErrorMessageResourceName = messageResourceName;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            String cpf = Convert.ToString(value);
            if (!regex.Match(cpf).Success)
            {
                return false;
            }

            // if it gets here than cpf must have 11 digits, so just remove the .'s and the -
            cpf = cpf.Replace(".", "").Replace("-", "");

            #region calculate the first verification digit

            String digit = "";
            int sum = 0;
            int verificationDigit = 0;

            for (int i = 0; i < 9; i++)
            {
                digit = (cpf.Substring(i, 1));
                sum = sum + (Int32.Parse(digit) * (10 - i));
            }

            verificationDigit = (11 - (sum % 11));

            if (verificationDigit > 9)
                verificationDigit = 0;

            if (verificationDigit != Int32.Parse(cpf.Substring(9, 1)))
                return false;

            #endregion

            #region calculate the second verification digit

            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                digit = (cpf.Substring(i, 1));
                sum = sum + (Int32.Parse(digit) * (11 - i));
            }

            verificationDigit = (11 - (sum % 11));

            if (verificationDigit > 9)
                verificationDigit = 0;

            if (verificationDigit != Int32.Parse(cpf.Substring(10, 1)))
                return false;

            #endregion

            return true;
        }
    }
}