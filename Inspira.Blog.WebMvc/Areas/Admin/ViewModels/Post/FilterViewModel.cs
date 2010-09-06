using System;
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