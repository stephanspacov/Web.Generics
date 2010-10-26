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
using Inspira.Blog.WebMvc.Areas.Admin.ViewModels.WebLog;
using System.Linq.Expressions;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using System.Reflection;
using Web.Generics.UserInterface.Components;

namespace Inspira.Blog.WebMvc.Areas.Admin.Controllers
{
    public class WebLogController : Controller
    {
		private GenericRepository<WebLog> WebLogs { get; set; }
		public WebLogController(GenericRepository<WebLog> webLogs)
		{
			this.WebLogs = webLogs;
		}

        public ActionResult Index(ListViewModel viewModel)
        {
			// obj
			var webLogGridBuilder = new GridBuilder(viewModel.DefaultGrid);

			// parameters -> data source
			var webLogDataSource = webLogGridBuilder.GetDataSourceByParameters(WebLogs).ToList();

			// data source -> grid
			webLogGridBuilder.Populate(webLogDataSource);

			// obj
			var postGridBuilder = new GridBuilder(viewModel.Wrapper.OutroGrid);

			// parameters -> data source
			var postDataSource = postGridBuilder.GetDataSourceByParameters(from w in WebLogs.Query() from p in w.Posts select p).ToList();

			// data source -> grid
			postGridBuilder.Populate(postDataSource);

			return View(viewModel);
        }
    }
}