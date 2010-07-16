using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc.Areas.Admin.ViewModels.WebLog;
using Web.Generics.UserInterface.HtmlHelpers;
using System.Linq.Expressions;
using Inspira.Blog.DomainModel;

namespace Inspira.Blog.WebMvc.Areas.Admin.Controllers
{
    public class WebLogController : Controller
    {
        public ActionResult Index(ListViewModel viewModel)
        {
			Expression<Func<WebLog, Boolean>> exp = GetExpression(viewModel.DefaultGrid);
			viewModel = new ListViewModel
			{
				DefaultGrid = new GridDataBinder().GetGrid(new [] {
					new SuperHero { ID = 1, Name="raphael", Surname="cruzeiro", Username="rc" },
					new SuperHero { ID = 2, Name="na", Surname="lee", Username="woohoo" },
				}),
				Wrapper = new Wrapper { 
					OutroGrid = new Grid
					{
						Columns = new[] {
							new GridColumn { PropertyName = "ID", HeaderText="Code" },
							new GridColumn { PropertyName = "Oxe", HeaderText="Oxente" },
							new GridColumn { PropertyName = "Arr", HeaderText="Arretado" },
						},
						Rows = new[] {
							new GridRow(11, "marcio", "canuto"),
							new GridRow(22, "luiz", "gonzaga"),
							new GridRow(33, "josé", "agripino"),
						},
						AllowSorting = true
					}
				},
			};
			return View(viewModel);
        }

		private Expression<Func<WebLog, Boolean>> GetExpression(Grid grid)
		{
			return null;
		}

		public class SuperHero
		{
			public Int32 ID { get; set; }
			public String Username { get; set; }
			public String Name { get; set; }
			public String Surname { get; set; }			
		}
    }
}