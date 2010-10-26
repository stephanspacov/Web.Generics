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
using Inspira.Blog.WebMvc3.Areas.Admin.Models;
using Web.Generics.DomainServices;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.UserInterface.Models;

namespace Inspira.Blog.WebMvc3.Areas.Admin.Controllers
{
    public class WebLogController : Controller
    {
        public GenericService<User> userService;
        public GenericService<WebLog> webLogService;

        public WebLogController(GenericService<User> userService, GenericService<WebLog> webLogService)
        {
            this.userService = userService;
            this.webLogService = webLogService;
        }

        public ActionResult List(WebLogViewModel viewModel)
        {
            var blogs = webLogService.Select(null, viewModel.WebLogGrid);

            viewModel.WebLogGrid.Columns = GridColumn.Create("Title", "Título", "Creator", "Criador", "CreatedAt", "Criado em");
            viewModel.WebLogGrid.DataSource = blogs;
            viewModel.WebLogGrid.DataBind();

            var users = userService.Select(null, viewModel.UserGrid);

            viewModel.UserGrid.Columns = GridColumn.Create("Name", "Nome completo", "Salary", "Salário", "BirthDate", "Data de nascimento");
            viewModel.UserGrid.DataSource = users;
            viewModel.UserGrid.DataBind();

            return View(viewModel);
        }
    }
}
