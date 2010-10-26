using System;
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
