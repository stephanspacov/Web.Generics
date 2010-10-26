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
using System.Web.Mvc;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;

namespace Inspira.Blog.WebMvc.Controllers
{
    public class TestController : Controller
    {
        private readonly GenericRepository<WebLog> webLogRepository;
        public TestController(GenericRepository<WebLog> webLogRepository)
        {
            this.webLogRepository = webLogRepository;
        }

        WebLog[] webLogs = new WebLog[5];
        public ActionResult InsertPosts()
        {
            var user = new User { ID = 32, Name="oi" };
            for (Int32 i = 0; i < 5; i++) {
                webLogs[i] = new WebLog { ID = i + 32, Title = "titulo " + i, CreatedAt=DateTime.Now, Collaborators = new[] { user } };
                this.webLogRepository.SaveOrUpdate(webLogs[i]);
            }

            for (Int32 i = 0; i < 100; i++)
            {
                CreateRandomPost(i);
            }
            this.webLogRepository.SaveChanges();
            return Content("Posts inseridos");
        }

        private void CreateRandomPost(Int32 i)
        {
            var rnd = new Random(i * (int)DateTime.Now.Ticks);
            var post = new Post
            {
                CreatedAt = DateTime.Now.AddSeconds(-new Random().Next()),
                IsPublished = rnd.NextDouble() < 0.5,
                LastUpdatedAt = DateTime.Now.AddMilliseconds(-rnd.Next()),
                PublishedAt = DateTime.Now.AddMilliseconds(-rnd.Next()),
                Text = "Texto do post aleatório " + Guid.NewGuid().ToString(),
                Title = "Post aleatório " + Guid.NewGuid().ToString(),
                WebLog = webLogs[rnd.Next(5)]
            };
            post.WebLog.Posts.Add(post);
        }

        public ActionResult Cadastro()
        {
            return View();
        }
    }
}