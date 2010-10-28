using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inspira.Blog.WebMvc3.Models.Identity
{
    public class ForgetPassword
    {
        [Display(Name = "User Name or E-mail")]
        public string UserNameOrEmail { get; set; }
    }
}
