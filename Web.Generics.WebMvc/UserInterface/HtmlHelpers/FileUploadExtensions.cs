using System;
using System.Text;
using System.Web.Mvc;

namespace Web.Generics.HtmlHelpers
{
    public static class FileUploadExtensions
    {
        /// <summary>
        /// Defines all options for <see cref="FileUploadExtensions.Uploadify"/>.
        /// </summary>
        public class UploadifyOptions
        {
            #region Public Properties

            /// <summary>
            /// The URL to the action that will process uploaded files.
            /// </summary>
            public string UploadUrl { get; set; }

            /// <summary>
            /// The file extensions to accept.
            /// </summary>
            public string FileExtensions { get; set; }

            /// <summary>
            /// Description corresponding to <see cref="FileExtensions"/>.
            /// </summary>
            public string FileDescription { get; set; }

            /// <summary>
            /// The ASP.NET forms authentication token.
            /// </summary>
            /// <example>
            /// You can get this in a view using:
            /// <code>
            /// Request.Cookies[FormsAuthentication.FormsCookieName].Value
            /// </code>
            /// You should check for the existence of the cookie before accessing
            /// its value.
            /// </example>
            public string AuthenticationToken { get; set; }

            /// <summary>
            /// The name of a JavaScript function to call if an error occurs
            /// during the upload.
            /// </summary>
            public string ErrorFunction { get; set; }

            /// <summary>
            /// The name of a JavaScript function to call when an upload
            /// completes successfully. 
            /// </summary>
            public string CompleteFunction { get; set; }

            #endregion
        }

        /// <summary>
        /// Renders JavaScript to turn the specified file input control into an 
        /// Uploadify upload control.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string Uploadify(this HtmlHelper helper, string name, UploadifyOptions options)
        {
            string scriptPath = null;//helper.ResolveUrl("~/Scripts/Uploadify/");

            StringBuilder sb = new StringBuilder();
            //Include the JS file.
            //sb.Append(helper.ScriptInclude("~/Scripts/Uploadify/jquery.uploadify.v2.1.0.min.js"));

            //Dump the script to initialze Uploadify
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("$(document).ready(function() {");
            sb.AppendFormat("initUploadify($('#{0}'),'{1}','{2}','{3}','{4}','{5}',{6},{7});", name, options.UploadUrl,
                            scriptPath, options.FileExtensions, options.FileDescription, options.AuthenticationToken,
                            options.ErrorFunction ?? "null", options.CompleteFunction ?? "null");
            sb.AppendLine();
            sb.AppendLine("});");
            sb.AppendLine("</script");

            return sb.ToString();
        }
    }
}
