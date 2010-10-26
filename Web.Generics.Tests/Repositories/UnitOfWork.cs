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
using System.Data.Objects;
using Web.Generics.Infrastructure.DataAccess;
using Web.Generics.ApplicationServices.DataAccess;

namespace Web.Generics.Tests.Repositories
{
    public class UnitOfWork
    {
        private readonly IRepositoryContext context;
        public UnitOfWork(IRepositoryContext context)
        {
            this.context = context;
            this.Posts = new PostRepository(context);
            this.Tags = new TagRepository(context);
        }

        public IPostRepository Posts { get; set; }
        public TagRepository Tags { get; set; }

        public void SaveChanges() {
            context.SaveChanges();
        }
    }
}
