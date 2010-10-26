using System;
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
