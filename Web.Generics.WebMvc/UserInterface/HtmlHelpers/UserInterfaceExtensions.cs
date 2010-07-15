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
    }
}