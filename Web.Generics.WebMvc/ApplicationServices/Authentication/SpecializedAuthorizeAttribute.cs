using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web;
using System.Web.Mvc;

namespace Web.Generics.WebMvc
{
    // Raphael Cruzeiro / Zhao Qiyan 2010-08-12
    /// <summary>
    /// A specialized AuthorizeAttribute for handling custom and multiple login urls
    /// </summary>
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
                Deny(filterContext);
            }
            else
            {
                if (!VerifyRoles(filterContext))
                    Deny(filterContext);
            }
        }

        private void Deny(AuthorizationContext filterContext)
        {
            // auth failed, redirect to login page
            if (AccessDeniedUrl != null && AccessDeniedUrl != String.Empty)
            {
                filterContext.Result = Redirect(filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri));
            }

            filterContext.Result = new HttpUnauthorizedResult();
        }

        private bool VerifyRoles(AuthorizationContext filterContext)
        {
            bool result = true;

            string[] roles = Roles.Trim().Split(new char[] { ',' });

            foreach (string role in roles)
                if (!filterContext.HttpContext.User.IsInRole(role))
                    result = false;

            return result;
        }

        private ActionResult Redirect(string returnUrl)
        {
            HttpContext.Current.Response.Redirect(string.Concat(HttpContext.Current.Request.ApplicationPath + AccessDeniedUrl, "?ReturnUrl=", returnUrl));
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
