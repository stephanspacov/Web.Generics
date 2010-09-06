using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.UserInterface.HtmlHelpers;
using System.Linq.Expressions;

namespace Web.Generics.DomainServices
{
    public class SortingInfo<T>
    {
        public Boolean SortingEnabled { get; set; }
        public Expression<Func<T, Object>> SortProperty { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
