using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using Web.Generics.Util;

namespace Web.Generics.UserInterface.Compression
{
    public class GzipAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Gzip.GZipEncodePage(HttpContext.Current.Request, HttpContext.Current.Response);
            base.OnActionExecuted(filterContext);
        }
    }
}
