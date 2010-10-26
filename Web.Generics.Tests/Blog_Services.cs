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
using System.Linq;
using Inspira.Blog.DomainModel;
using Inspira.Blog.Infrastructure.DataAccess.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.Infrastructure.DataAccess.EntityFramework;

namespace Inspira.Blog.Tests
{
    [TestClass]
    public class Blog_Services
    {
        GenericRepository<WebLog> WebLogs;
        String webLogID = Guid.NewGuid().ToString();
        String postID = Guid.NewGuid().ToString();

        [TestInitialize]
        public void Initialize()
        {
            var context = new EntityFrameworkRepositoryContext(new BlogContext());
            WebLogs = new GenericRepository<WebLog>(context);

            var title = Guid.NewGuid().ToString();

            var user = new User { Name = "user " + title };
            var webLog = new WebLog { Title = webLogID, CreatedAt = DateTime.Now };
            var post = new Post { Title = postID, Text = "oi", CreatedAt = DateTime.Now, LastUpdatedAt = DateTime.Now, PublishedAt = DateTime.Now };

            webLog.Collaborators.Add(user);
            user.AddBlog(webLog);
            webLog.Posts.Add(post);
            post.WebLog = webLog;

            WebLogs.SaveOrUpdate(webLog);
        }

        [TestMethod]
        public void List_Posts_By_Date_Range()
        {
            //WebLogs.Where(w => w.ID == 12).SingleOrDefault().Posts.Where();

            Assert.Inconclusive();
        }

        [TestMethod]
        public void Add_Comment_In_Post()
        {
            var comment = new Comment();
            WebLogs.SingleOrDefault(w => w.ID == 12).Posts.SingleOrDefault(p => p.ID == 144).Comments.Add(comment);

            var item = new WebLog();
            item.Title = "jhji";
            //WebLogs.Add(item);

            WebLogs.SaveChanges();

            Assert.Inconclusive();
        }
    }
}
