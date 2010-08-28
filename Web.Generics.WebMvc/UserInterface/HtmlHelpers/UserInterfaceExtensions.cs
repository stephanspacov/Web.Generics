using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Text;

namespace Web.Generics.UserInterface.HtmlHelpers
{
    public static class UserInterfaceExtensions
    {
        public static String List<T>(this HtmlHelper helper, IEnumerable<T> items, String format, params Func<T, Object>[] properties)
        {
            var returnValue = "<ul>";
            items.Select(i => String.Format(format, properties.Select(p => p.Invoke(i)).ToArray())).ToList().ForEach(i => returnValue += "<li>" + i + "</li>");
            return returnValue + "</ul>";
        }

        public static String Grid(this HtmlHelper helper, IEnumerable items)
        {
            var returnValue = "<table><tr><td>";
            returnValue += "Aqui vem o grid";
            return returnValue + "</td></tr></table>";
        }

        public static String Grid<T>(this HtmlHelper helper, IEnumerable<T> items)
        {
            var returnValue = "<table><tr><td>";
            returnValue += "Aqui vem o grid";
            return returnValue + "</td></tr></table>";
        }

		public static String Grid<TViewModel, TGrid>(this HtmlHelper<TViewModel> htmlHelper, Expression<Func<TViewModel, TGrid>> gridExpression) where TGrid : Grid
		{
			var grid = gridExpression.Compile().Invoke(htmlHelper.ViewData.Model);

			if (grid == null) throw new ArgumentNullException();

			var gridName = gridExpression.Body.ToString();
			gridName = gridName.Substring(gridName.IndexOf(".") + 1);

			var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
			var buffer = new StringBuilder();

			var allowPaging = grid.AllowPaging;
			var allowSorting = grid.AllowSorting;

			if (allowPaging || allowSorting) buffer.Append("<form action='' class='form'>");
			if (allowSorting) buffer.AppendFormat("<input type='hidden' name='{0}.SortOrder' value='{1}' />", gridName, grid.SortOrder);

			buffer.Append(@"<table class='table'><tbody><tr><th class='first'><input class='checkbox toggle' type='checkbox' /></th>");
			foreach (var column in grid.Columns) {
				if (allowSorting)
				{
					buffer.AppendFormat("<th><button type='submit' name='{0}.SortProperty' value='{1}'>{2}</button> </th>", gridName, column.PropertyName, column.HeaderText);
				}
				else
				{
					buffer.AppendFormat("<th>{0} </th>", column.HeaderText);
				}
			}
			buffer.Append("<th class='last'>&nbsp; </th></tr>");

			for (var i = 0; i < grid.Rows.Count; i++)
			{
				var row = grid.Rows[i];
				buffer.AppendFormat("<tr class='{0}'><td><input class='checkbox' name='{1}.SelectedRowKeys' value='{2}' type='checkbox' /></td>", (i % 2 == 0) ? "even" : "odd", gridName, row.KeyValue);
				foreach (var cell in row.Cells)
				{
					buffer.AppendFormat("<td>{0}</td>", cell.Text);
				}
				buffer.Append("<td class='grid-buttons'>");

				buffer.AppendFormat("<a href='{0}'><img src='{1}' alt='Edit' /></a>", urlHelper.Action("Edit"), urlHelper.Content("~/assets/img/common/icons/edit-icon.gif"));
				buffer.AppendFormat("<a href='{0}'><img src='{1}' alt='Delete' /></a>", urlHelper.Action("Delete"), urlHelper.Content("/assets/img/common/icons/delete-icon.gif"));
				buffer.Append("</td></tr>");
			}
			buffer.Append("</tbody></table>");

			// pager
			/*
			buffer.AppendFormat("<div class='actions-bar wat-cf'><div class='actions'><button class='button' type='submit' name='{0}.Delete'><img src='{1}' alt='Delete' />Delete </button></div><div class='pagination'>", gridName, urlHelper.Content("/assets/img/common/icons/delete-icon.gif"));
			buffer.Append("<span class='disabled prev_page'>« Previous</span>");
			buffer.Append("<span class='current'>1</span><a rel='next' href='#'>2</a>");
			buffer.Append("<a rel='next' class='next_page' href='#'>Next »</a>");
			buffer.Append("</div></div>");
			*/

			if (allowPaging || allowSorting) buffer.Append("</form>");

			return buffer.ToString();
		}

		public static String FilterForGrid<TViewModel, TGrid>(this HtmlHelper<TViewModel> htmlHelper, Expression<Func<TViewModel, TGrid>> gridExpression, TViewModel viewModel) where TGrid : Grid
		{
			var gridName = gridExpression.Body.ToString();
			gridName = gridName.Substring(gridName.IndexOf(".") + 1);

			var sb = new StringBuilder();
			sb.Append("<form class='form' method='get' action='#'>");
			sb.AppendFormat("<div class='group'><label for='{0}.{1}' class='label'>Search</label><input type='text' class='text_field' name='{0}.{1}' id='{0}.{1}' /><span class='description'></span></div>", gridName, "SearchQuery");

			foreach (var property in typeof(TViewModel).GetProperties())
			{
				sb.AppendFormat("<div class='group'><label for='{0}.{1}' class='label'>{2}</label><input type='text' class='text_field' name='{0}.{1}' id='{0}.{1}' /><span class='description'></span></div>", gridName, property.Name, property.Name);
			}
			sb.Append("</form>");
			return sb.ToString();
		}
	}
}