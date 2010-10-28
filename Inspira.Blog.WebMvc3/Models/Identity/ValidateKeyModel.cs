using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inspira.Blog.WebMvc3.Models.Identity
{
    public class ValidateKeyModel
    {
        [Required]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required]
        [Display(Name = "Confirm")]
        public string Confirm { get; set; }
        [Display(Name = "Key")]
        public string Key { get; set; }
    }
}
