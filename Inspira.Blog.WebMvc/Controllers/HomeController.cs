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
using Inspira.Blog.WebMvc.ViewModels;
using Inspira.Blog.DomainModel;
using Web.Generics.DomainServices;
using Inspira.Blog.DomainServices;
using Web.Generics.Util;
using Web.Generics.UserInterface.Compression;

namespace Inspira.Blog.WebMvc.Controllers
{
    [HandleError]
    [Gzip]
    public class HomeController : Controller
    {
        private readonly WebLogService webLogService;
        private readonly PostService postService;
        public HomeController(WebLogService webLogService, PostService postService)
        {
            this.webLogService = webLogService;
            this.postService = postService;
        }

        public ActionResult Index()
        {
			if (User.Identity.IsAuthenticated) return RedirectToRoute("Admin");

            var viewModel = new HomeIndexViewModel();
            viewModel.WebLogs = this.webLogService.Select();
            viewModel.LastWebLogs = this.webLogService.SelectRecent(2);
            viewModel.LastPublishedPosts = this.postService.SelectLastPublishedPosts(5);

            return View(viewModel);
        }
    }
}
