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
using System.Web.Mvc;

namespace Inspira.Blog.WebMvc.Areas.Admin.ViewModels.Post
{
	public class FilterViewModel
	{
		public Published? Published { get; set; }
		public DateTime? CreatedAtStart { get; set; }
		public DateTime? CreatedAtEnd { get; set; }
		public Int32 BlogID { get; set; }
		public String SearchQuery { get; set; }
		public IEnumerable<SelectListItem> PublishedSelectList { get; set; }
		public IEnumerable<SelectListItem> BlogSelectList { get; set; }

		internal Func<Inspira.Blog.DomainModel.Post, Boolean> GetExpression()
		{
			return p =>
			(
				!Published.HasValue
				||
				Published.Value == Post.Published.Yes && p.PublishedAt != null && p.PublishedAt < DateTime.Now
				||
				Published.Value == Post.Published.No && p.PublishedAt == null
			)
			&&
			(
				!CreatedAtStart.HasValue || p.CreatedAt > CreatedAtStart.Value
				&&
				!CreatedAtEnd.HasValue || p.CreatedAt < CreatedAtEnd
			);
			/*
			&&
			(
				BlogID == 0
				||
				p.WebLog.ID == BlogID
			)
			&&
			(
				SearchQuery == null
				||
				p.Title.Contains(SearchQuery)
				||
				p.Text.Contains(SearchQuery)
			);
 */
		}
	}
}