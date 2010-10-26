using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Generics.UserInterface.Models;
using Inspira.Blog.DomainModel;

namespace Inspira.Blog.WebMvc3.Areas.Admin.Models
{
    public class WebLogViewModel
    {
        public WebLogViewModel()
        {
            this.WebLogGrid = new Grid();
            this.UserGrid = new Grid();
        }

        public Grid WebLogGrid { get; set; }
        public Grid UserGrid { get; set; }
    }
}