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
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc.ViewModels.Posts;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;

namespace Inspira.Blog.WebMvc.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/
        private readonly GenericRepository<WebLog> WebLogs;
        public PostController(GenericRepository<WebLog> WebLogs)
        {
            this.WebLogs = WebLogs;
        }

        public ActionResult Index()
        {
            var viewModel = new IndexViewModel();

            var post = new Post();
            post.Title = "titulo";
            post.Text = "texto";
            post.PublishedAt = DateTime.Now.AddDays(-3);

            viewModel.PostsPublicados.Add(post);

            return View(viewModel);
        }

        public ActionResult Details(Int32 id)
        {
            var viewModel = new DetailsViewModel();
            var webLog = WebLogs.Where(w => w.ID == 1).SingleOrDefault(); // TODO
            var post = webLog.Posts.Where(p => p.ID == id).SingleOrDefault();

            viewModel.Post = post;

            return View(viewModel);
        }
    }
}
