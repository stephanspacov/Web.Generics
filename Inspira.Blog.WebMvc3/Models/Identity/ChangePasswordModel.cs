using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inspira.Blog.WebMvc3.Models.Identity
{
    public class ChangePassword
    {
        [Required]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
