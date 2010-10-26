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
    public class PostalCodeAttribute : ValidationAttribute
    {
        private readonly Regex regex = new Regex(@"\d{5}-\d{3}");
        public PostalCodeAttribute() { }

        public PostalCodeAttribute(String messageResourceName)
        {
            ErrorMessageResourceName = messageResourceName;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            String code = Convert.ToString(value);
            if (!regex.Match(code).Success)
            {
                return false;
            }

            return true;
        }
    }
}
