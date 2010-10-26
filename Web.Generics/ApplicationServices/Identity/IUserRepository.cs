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
    public interface IUserRepository<T>
    {
        RegisterStatus VerifyUniqueUser(T user);
        void InsertUser(T user);
        void SaveOrUpdate(T user);
        T Select(string username, string password);
        T Select(string email);
        bool ChangePassword(string username, string currentPassword, string newPassword);
        bool ChangePassword(string username, string newPassword);
        bool SetValidationKey(string email, string validationKey);
    }
}
