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
            var users = new[] {
                new User { Name="Loreto", Salary=1234.45M, BirthDate=DateTime.Now.AddYears(-28) },
                new User { Name="Thiagão", Salary=99234.45M, BirthDate=DateTime.Now.AddYears(-28) },
            };

            // TODO: ler de um serviço ou repositório
            if (viewModel.Grid.SortingInfo.GetSortOrder() == SortOrder.Descending)
            {
                Func<User, Object> exp = viewModel.Grid.SortingInfo.GetSortExpression<User>().Compile();
                users = users.OrderByDescending(exp).ToArray();
            }

            var grid = viewModel.Grid;
            grid.Columns = GridColumn.Create("Name", "Nome completo", "Salary", "Salário", "BirthDate", "Data de nascimento");
            grid.DataSource = users;
            grid.DataBind();

            grid.SortingInfo.SortingEnabled = true;
            grid.PagingInfo.PagingEnabled = true;

            viewModel.Grid = grid;
            return View(viewModel);
        }

        public ActionResult List2()
        {
            var viewModel = new UserViewModel
            {
                Grid = new Grid
                {
                    PagingInfo = new PagingInfo { TotalItemCount = 44, PagingEnabled = true, PageSize = 10, PageIndex = 2 },
                    Columns = new[] {
                            new GridColumn { HeaderText = "Column 1", PropertyName = "PropertyName" },
                            new GridColumn { HeaderText = "Column 2", PropertyName = "PropertyName2" },
                    },
                    Rows = new[] {
                        new GridRow("Cell value 1-1", "Cell value 1-2") { KeyValue="08" },
                        new GridRow("Cell value 2-1", "Cell value 2-2") { KeyValue="18" },
                        new GridRow("Cell value 3-1", "Cell value 3-2") { KeyValue="28" },
                    }
                }
            };
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
