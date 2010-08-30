using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Web.Generics.UserInterface.Validators;

namespace Inspira.Blog.WebMvc.ViewModels.Account
{
    public class SignUpViewModel
    {
        [Required]
        public String Name { get; set; }

        [Required]
        [Email]
        public String Email { get; set; }

        [Required]
        public String BlogTitle { get; set; }

        [Required]
        public String Password { get; set; }

		public bool ShowMinimumCharaterLengthMessage { get; set; }

		public Int32 MinimumCharaterLength { get; set; }
	}
}