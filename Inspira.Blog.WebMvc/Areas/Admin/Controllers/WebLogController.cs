using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc.Areas.Admin.ViewModels.WebLog;
using Web.Generics.UserInterface.HtmlHelpers;
using System.Linq.Expressions;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using System.Reflection;

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