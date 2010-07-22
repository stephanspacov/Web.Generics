using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc.Areas.Admin.ViewModels.Post;


namespace Inspira.Blog.WebMvc.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Admin/PostCreate/

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            return View();
        }

    }
}
