using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.Html;
using System.Web.Mvc;


namespace Web.Generics.UserInterface.Extensions
{
    public static class SelectListHelper
    {
        public static SelectListItem ToSelectListItem<T, TResult, TResult2>(T obj, Func<T, TResult> value, Func<T, TResult2> text)
        {
            SelectListItem result = new SelectListItem();
            result.Value = value.Invoke(obj).ToString();
            result.Text = text.Invoke(obj).ToString();
            return result;
        }

        public static SelectList ToSelectList<T, TResult, TResult2>(this IList<T> list, Func<T, TResult> value, Func<T, TResult2> text)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            list.ToList<T>().ForEach(item => result.Add(ToSelectListItem(item, value, text)));

            return new SelectList(result, "Value", "Text");
        }
    }
}
