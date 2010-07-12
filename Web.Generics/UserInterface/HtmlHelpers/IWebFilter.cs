using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Web.Generics.HtmlHelpers
{
    public interface IWebFilter<T>
    {
        Expression<Func<T, Boolean>> FilterExpression { get; set; }
        [DisplayName("Buscar por")]
        String SearchQuery { get; set; }
    }
}
