/*
Copyright 2010 Inspira Tecnologia.
All Rights Reserved.

Contact: Thiago Alves <thiago.alves@inspira.com.br>

This file is part of Web.Generics

Web.Generics is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Web.Generics is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with Web.Generics.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Generics.UserInterface.Models;

namespace Inspira.Blog.WebMvc.Areas.Admin.ViewModels.Post
{
	public class IndexViewModel
	{
		public IndexViewModel()
		{
			this.AllPosts = new Grid();
			this.Filter = new FilterViewModel();
		}

		public Grid AllPosts { get; set; }
		public FilterViewModel Filter { get; set; }
	}
}