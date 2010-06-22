using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.HtmlHelpers
{
    public interface IWebPager
    {
        Int32 PageIndex { get; set; }
        Int32 PageSize { get; set; }
        Int32 NumberOfPages { get; }
        Int32 TotalItemCount { get; set; }
        Int32 PageStartIndex { get; }
        Boolean HasPrevious { get; }
        Boolean HasNext { get; }
        Boolean AllowPaging { get; set; }
    }
}
