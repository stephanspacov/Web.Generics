using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web;
using System.Web.Mvc;

namespace Web.Generics
{
    public class SpecializedAuthorizeAttribute : AuthorizeAttribute
    {
        public string AccessDeniedUrl { get; set; }

        public void CacheValidationHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (AuthorizeCore(filterContext.HttpContext))
            {
                SetCachePolicy(filterContext);
            }
            else if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // auth failed, redirect to login page
                if (AccessDeniedUrl != null && AccessDeniedUrl != String.Empty)
                    filterContext.Result = Redirect();
                else
                    filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
            }
        }

        private ActionResult Redirect()
        {
            HttpContext.Current.Response.Redirect(AccessDeniedUrl);
            return null;
        }

        protected void SetCachePolicy(AuthorizationContext filterContext)
        {
            // ** IMPORTANT **
            HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
            cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            cachePolicy.AddValidationCallback(CacheValidationHandler, null /* data */);
        }

    }
}
