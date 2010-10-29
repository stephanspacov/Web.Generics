using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Web.Generics.UserInterface.Validators;

namespace Inspira.Blog.WebMvc3.Models.Identity
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Name")]
        public String Name { get; set; }
        [Required]
        [Display(Name = "User name")]
        public String Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public String Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        public String ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        [Email]
        public String Email { get; set; }
    }
}