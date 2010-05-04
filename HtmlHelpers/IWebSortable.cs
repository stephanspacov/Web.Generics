using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Web.Generics.HtmlHelpers
{
    public interface IWebSortable
    {
        void DefineSortProperty<TModel, TValue>(Expression<Func<TModel, TValue>> expression);
        String SortProperty { get; set; }
        SortOrder SortOrder { get; set; }
        Boolean AllowSorting { get; set; }
    }
}
