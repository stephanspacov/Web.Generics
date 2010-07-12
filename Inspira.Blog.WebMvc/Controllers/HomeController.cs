using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc.ViewModels;
using Inspira.Blog.DomainModel;
using Web.Generics.DomainServices;
using Inspira.Blog.DomainServices;

namespace Inspira.Blog.WebMvc.Controllers
{
    [HandleError]
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
            var viewModel = new HomeIndexViewModel();
            viewModel.WebLogs = this.webLogService.Select();
            viewModel.LastWebLogs = this.webLogService.SelectRecent(2);
            viewModel.LastPublishedPosts = this.postService.SelectLastPublishedPosts(5);
            return View(viewModel);
        }
    }
}
