using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.UserInterface.HtmlHelpers
{
    public class PagerInfo
    {
        public Int32 TotalItemCount { get; set; }
        public Int32 PageSize { get; set; }
        public Int32 PageIndex { get; set; }
        public Int32 GetNumberOfPages()
        {
            return TotalItemCount / PageSize;
        }
    }
}
