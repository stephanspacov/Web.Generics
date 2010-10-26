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
using FluentNHibernate.Automapping.Alterations;
using Inspira.Blog.DomainModel;
using FluentNHibernate.Automapping;
using FluentNHibernate;

namespace Inspira.Blog.Infrastructure.DataAccess.FluentNHibernate.Overrides
{
    public class UserOverride : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.Map(u => u.AdditionalInfo).Nullable();
            mapping.Map(u => u.NumberOfChildren).Nullable();
            mapping.Map(u => u.BirthDate).Nullable();
            mapping.Map(u => u.Cpf).Nullable();
            mapping.Map(u => u.Phone).Nullable();
            mapping.Map(u => u.Photo).Nullable();
            mapping.Map(u => u.Salary).Nullable();
            mapping.Map(u => u.Resume).Nullable();
            mapping.HasMany<WebLog>(Reveal.Member<User>("ownedBlogs")).AsBag().LazyLoad().Cascade.All();
            //mapping.HasManyToMany(u => u.AssociatedBlogs).Table("User_WebLog");
            // mapping.Component(u => u.Address);
        }
    }
}
