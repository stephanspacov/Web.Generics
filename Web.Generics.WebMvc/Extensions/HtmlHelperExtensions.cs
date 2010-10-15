using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static string ConvertRelativeUrlToAbsoluteUrl(this HtmlHelper html, string relativeUrl)
        {
            string url = relativeUrl.Replace("~", "");

            string host = HttpContext.Current.Request.Url.Host;

            if (HttpContext.Current.Request.Url.Port != 80)
                host = String.Format("{0}:{1}", host, HttpContext.Current.Request.Url.Port);

            Page page = new Page();

            if (HttpContext.Current.Request.IsSecureConnection)
                return string.Format("https://{0}{1}", host, page.ResolveUrl(url));
            else
                return string.Format("http://{0}{1}", host, page.ResolveUrl(url));
        }
    }
}