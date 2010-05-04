using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Web.Generics.HtmlHelpers
{
    public static class GridHelper
    {
        public static String RenderGrid<T>(this HtmlHelper helper, WebGrid<T> grid)
        {
            return "Renderizou";
        }
    }
}
