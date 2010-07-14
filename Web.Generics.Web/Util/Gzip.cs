using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Web.Generics.Util
{
    public static class Gzip
    {
        public static void GZipEncodePage(HttpRequest Request, HttpResponse Response)
        {
            if (!IsGZipSupported(Request)) return;
            string AcceptEncoding = Request.Headers["Accept-Encoding"];

            if (AcceptEncoding.Contains("gzip"))
            {
                Response.Filter = new System.IO.Compression.GZipStream(Response.Filter, System.IO.Compression.CompressionMode.Compress);
                Response.AppendHeader("Content-Encoding", "gzip");
            }
            else
            {
                Response.Filter = new System.IO.Compression.DeflateStream(Response.Filter, System.IO.Compression.CompressionMode.Compress);
                Response.AppendHeader("Content-Encoding", "deflate");
            }
        }

        private static bool IsGZipSupported(HttpRequest Request)
        {
            string AcceptEncoding = Request.Headers["Accept-Encoding"];
            if (!string.IsNullOrEmpty(AcceptEncoding))
                if (AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate"))
                    return true;
            return false;
        }
    }
}
