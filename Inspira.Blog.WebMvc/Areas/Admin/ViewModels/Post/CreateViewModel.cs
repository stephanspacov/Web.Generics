using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Web.Generics.UserInterface.Validators;

namespace Inspira.Blog.WebMvc.Areas.Admin.ViewModels.Post
{
    public class CreateViewModel
    {
            [Required]
            public String PostTitle { get; set; }

            [Required]
            public String TextArea { get; set; }
    }
}