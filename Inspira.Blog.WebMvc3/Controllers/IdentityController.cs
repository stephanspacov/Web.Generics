using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc3.Models.Identity;
using Web.Generics.ApplicationServices.Identity;
using Inspira.Blog.DomainModel;

namespace Inspira.Blog.WebMvc3.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IdentityService<User> identityService;
        public IdentityController(IdentityService<User> identityService)
        {
            this.identityService = identityService;
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            if (this.identityService.Validate(viewModel.Username, viewModel.Password))
            {
                // TODO: encapsulate this in a service (makes it easier to test up)
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(viewModel.Username, viewModel.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // invalid credentials
                ModelState.AddModelError("INVALID_CREDENTIALS", "");
                return View();
            }
        }

        public ActionResult LogOff()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            User user = new User();
            user.Name = viewModel.Name;
            user.Username = viewModel.Username;
            user.Email = viewModel.Email;

            if (viewModel.Password != viewModel.ConfirmPassword)
            {
                ModelState.AddModelError("PASSWORD_AND_CONFIRM_DONT_MATCH", "");
            }
            else
            {
                var result = this.identityService.Register(user, u => u.Username, u => u.Email, u => u.Password, viewModel.Password);

                if (result == RegisterStatus.Success)
                {
                    // TODO: encapsulate this in a service
                    System.Web.Security.FormsAuthentication.RedirectFromLoginPage(user.Username, false); // user log on
                    return RedirectToAction("Index", "Home");
                }

                if (result == RegisterStatus.UsernameAlreadyExists)
                {
                    ModelState.AddModelError("USER_ALREADY_EXISTS", "");
                }

                if (result == RegisterStatus.EmailAlreadyExists)
                {
                    ModelState.AddModelError("EMAIL_ALREADY_EXISTS", "");
                }
            }

            return View(viewModel);
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(String usernameOrEmail)
        {
            var validationKey = this.identityService.GenerateValidationKey(usernameOrEmail);
            if (validationKey == null)
            {
                ModelState.AddModelError("USER_NOT_FOUND", "");
                return View();
            }
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
            if (!ModelState.IsValid) return View(viewModel);
            
            if (viewModel.NewPassword != viewModel.ConfirmNewPassword)
            {
                ModelState.AddModelError("PASSAWORD_AND_CONFIRM_DONT_MACHT", "");                
            }
            else
            {
                var userName = User.Identity.Name;
                var result = this.identityService.ChangePassword(userName, viewModel.CurrentPassword, viewModel.NewPassword);
                
                if (result == PasswordChangeStatus.Success)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (result == PasswordChangeStatus.InvalidCurrentPassword)
                {
                    ModelState.AddModelError("INVALID_CURRENT_PASSWORD", "");
                }

                if (result == PasswordChangeStatus.InvalidPassword)
                {
                    ModelState.AddModelError("INVALID_PASSWORD", "");
                }
            }

            return View(viewModel);
        }
    }
}