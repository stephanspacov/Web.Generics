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
using Web.Generics.ApplicationServices.DataAccess;
using Inspira.Blog.DomainModel;

namespace Web.Generics.Tests.UserInterface
{
    public class WebLogMockRepositoryContext : IRepositoryContext
    {
        List<WebLog> webLogs = new List<WebLog> {
                new WebLog { ID = 1, Title = "Post 1", CreatedAt = new DateTime(2008, 1, 1) },
                new WebLog { ID = 2, Title = "Post 2", CreatedAt = new DateTime(2004, 1, 1) },
                new WebLog { ID = 3, Title = "Post 3", CreatedAt = new DateTime(2010, 1, 1) },
                new WebLog { ID = 4, Title = "Post 4", CreatedAt = new DateTime(2007, 1, 1) },
                new WebLog { ID = 5, Title = "Post 5", CreatedAt = new DateTime(2009, 1, 1) },
                new WebLog { ID = 6, Title = "Post 6", CreatedAt = new DateTime(2005, 1, 1) },
                new WebLog { ID = 7, Title = "Post 7", CreatedAt = new DateTime(2006, 1, 1) },
            };

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate<T>(T obj) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete(object obj)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return (IQueryable<T>)this.webLogs.AsQueryable();
        }

        public T SelectById<T>(object id)
        {
            throw new NotImplementedException();
        }
    }
}
