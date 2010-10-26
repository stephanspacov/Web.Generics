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
using Web.Generics.UserInterface.Models;
using Web.Generics.DomainServices;
using Inspira.Blog.DomainServices;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.Infrastructure.DataAccess.NHibernate;

namespace Inspira.Blog.WebMvc3.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        UserService userService;
        public UserController()
        {
            this.userService = new UserService();
        }

        public ActionResult List(UserViewModel viewModel)
        {
            var users = new GenericService<User>(new GenericRepository<User>(new NHibernateRepositoryContext())).Select(null, viewModel.MyGrid);

            viewModel.MyGrid.Columns = GridColumn.Create("Name", "Nome completo", "Salary", "Salário", "BirthDate", "Data de nascimento");
            viewModel.MyGrid.DataSource = users;
            viewModel.MyGrid.DataBind();

            var blogs = new GenericService<WebLog>(new GenericRepository<WebLog>(new NHibernateRepositoryContext())).Select(null, viewModel.MyGrid2);
            viewModel.MyGrid2.Columns = GridColumn.Create("Title", "Título", "Creator", "Criador", "CreatedAt", "Criado em");
            viewModel.MyGrid2.DataSource = blogs;
            viewModel.MyGrid2.DataBind();

            return View("List", viewModel);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(UserViewModel userViewModel)
        {
            var isViewValid = userViewModel.Validate();
            if (isViewValid)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("_FORM", "Opa! Deu erro!");
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
