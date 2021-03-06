﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Generics.UserInterface.Models;
using Inspira.Blog.DomainModel;

namespace Inspira.Blog.WebMvc3.Areas.Admin.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.MyGrid = new Grid();
            this.MyGrid2 = new Grid();
        }

        public Grid MyGrid { get; set; }
        public Grid MyGrid2 { get; set; }
        public User User { get; set; }
        public String PasswordConfirmation { get; set; }
        public Boolean Validate()
        {
            return User.Username == "thiago";
        }
    }
}