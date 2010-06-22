using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.HtmlHelpers
{
    public interface IWebGrid<T> : IWebGrid
    {
        IEnumerable<T> DataSource { get; set; }
    }
}
