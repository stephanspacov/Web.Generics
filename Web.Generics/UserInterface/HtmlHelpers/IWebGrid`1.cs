using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Web.Generics.HtmlHelpers
{
    public interface IWebGrid<T> : IWebGrid
    {
        IEnumerable<T> DataSource { get; set; }
        Expression<Func<T, Boolean>> GetFilterExpression();
    }
}
