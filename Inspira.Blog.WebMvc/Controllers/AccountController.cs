using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspira.Blog.WebMvc.Controllers
{
    public partial class AccountController : Controller
    {
        public ActionResult SignUp()
        {
            return View();
        }
    }
}