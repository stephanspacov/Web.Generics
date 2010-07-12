using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

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
    }
}