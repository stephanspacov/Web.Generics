using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc3.Models.Identity;

namespace Inspira.Blog.WebMvc3.Controllers
{
    public class IdentityController : Controller
    {
        //
        // GET: /Identity/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            if (viewModel.Username == viewModel.Password)
            {
                // TODO: implement real user validation
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // invalid credentials
                ModelState.AddModelError("INVALID_CREDENTIALS", "");
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            if (viewModel.Username == "Stephan")
            {
                ModelState.AddModelError("USER_ALREADY_EXIST", "");
            }

            if (viewModel.Password != viewModel.ConfirmPassword)
            {
                ModelState.AddModelError("PASSAWORD_AND_CONFIRM_DONT_MACHT", "");
            }

            if (viewModel.Email == "joao@gmail.com")
            {
                ModelState.AddModelError("EMAIL_ALREADY_EXIST", "");
            }

            if (ModelState.IsValid)
            {
                return View();
            }
            else
            {
                return View(viewModel);
            }
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(String usernameoremail)
        {
            return RedirectToAction("ValidateKey");
        }

        public ActionResult ValidateKey()
        {
            ValidateKeyModel viewModel = new ValidateKeyModel();
            viewModel.Key = Request["Key"];

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ValidateKey(ValidateKeyModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            //custom validation
            if (viewModel.NewPassword != viewModel.Confirm)
            {
                ModelState.AddModelError("PASSAWORD_AND_CONFIRM_DONT_MACHT", "");
            }
            if (viewModel.Key == "123")
            {
                ModelState.AddModelError("INVALID_KEY", "");
            }

            //retorn of view
            if (ModelState.IsValid)
            {
                return View("ValidateKeySuccess");
            }
            else
            {
                return View(viewModel);
            }
        }

        public ActionResult ChangePassword()
        {
            ChangePassword viewModel = new ChangePassword();

            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword viewModel)
        {

            if (viewModel.NewPassword != viewModel.ConfirmNewPassword)
            {
                ModelState.AddModelError("INVALID_CREDENTIALS", "");

                return View();
            }

            return View();
        }
    }
}