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

﻿using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.ApplicationServices.InversionOfControl;
using Web.Generics.ApplicationServices.Identity;
using Inspira.Blog.Infrastructure.DataAccess.Repositories;
using NHibernate;
using Web.Generics;

namespace Inspira.Blog.Infrastructure.InversionOfControl
{
    public class InversionOfControlMapper : IInversionOfControlMapper
    {
        public void DefineMappings(IInversionOfControlContainer container)
        {
            container.RegisterType<IRepository<User>, GenericRepository<User>>();
            container.RegisterType<IUserRepository<User>, UserRepository>();
            container.RegisterType<IRepository<WebLog>, GenericRepository<WebLog>>();
            container.RegisterType<IRepository<Post>, GenericRepository<Post>>();
        }
    }
}
