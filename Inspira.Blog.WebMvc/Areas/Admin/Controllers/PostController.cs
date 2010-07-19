using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    }
}
