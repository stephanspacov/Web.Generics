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

﻿using System.Collections.Generic;
using System.Linq;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.DomainServices;

namespace Inspira.Blog.DomainServices
{
    public class PostService : GenericService<Post>
    {
        private readonly IRepository<Post> postRepository;
        public PostService(IRepository<Post> postRepository) : base(postRepository)
        {
            this.postRepository = postRepository;
        }

        public IList<Post> SelectLastPublishedPosts(int qty)
        {
            return this.postRepository.Query().Where(p => p.IsPublished == true).OrderByDescending(p => p.PublishedAt).Take(qty).ToList();
        }
    }
}
