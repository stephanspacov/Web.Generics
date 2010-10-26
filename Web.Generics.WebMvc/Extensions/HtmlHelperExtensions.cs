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