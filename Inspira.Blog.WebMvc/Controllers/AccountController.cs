using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc.ViewModels.Account;

namespace Inspira.Blog.WebMvc.Controllers
{
    public partial class AccountController : Controller
    {
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            // salvar dados no banco
            return View("SignUpConfirm");
        }
    }
}