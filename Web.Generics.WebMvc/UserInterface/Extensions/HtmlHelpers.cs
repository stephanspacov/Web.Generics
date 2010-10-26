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
using System.Web.Mvc;
using System.Collections;

namespace Web.Generics.UserInterface.Extensions
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString List<T>(this HtmlHelper helper, IList<T> list, String format, params Func<T, Object>[] propExpressions)
        {
            var result = "<ul>";
            for (var i = 0; i < list.Count; i++)
            {
                var obj = list[i];
                var lists = propExpressions.Select(f => f.Invoke(obj));
                result += String.Format("<li>" + format + "</li>", lists.ToArray());
            }
            result += "</ul>";
            return MvcHtmlString.Create(result);
        }
    }
}
