using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Web.Generics.HtmlHelpers
{
    public interface IWebFilter
    {
        IList<FilterCondition> FilterConditions { get; set; }
        [DisplayName("Buscar por")]
        String SearchQuery { get; set; }
    }
}
