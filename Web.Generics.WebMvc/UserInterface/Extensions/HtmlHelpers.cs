using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Collections;

namespace Web.Generics.UserInterface.Extensions
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString List<T>(this HtmlHelper helper, IList<T> list, String format, params Func<T, Object>[] propExpressions)
        {
            var result = "<ul>";
            for (var i = 0; i < list.Count; i++)
            {
                var obj = list[i];
                var lists = propExpressions.Select(f => f.Invoke(obj));
                result += String.Format("<li>" + format + "</li>", lists.ToArray());
            }
            result += "</ul>";
            return MvcHtmlString.Create(result);
        }
    }
}
