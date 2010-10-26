/*
Copyright 2010 Inspira Tecnologia.
All Rights Reserved.

Contact: Thiago Alves <thiago.alves@inspira.com.br>

This file is part of Web.Generics

Web.Generics is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Web.Generics is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with Web.Generics.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
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
