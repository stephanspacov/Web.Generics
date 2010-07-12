using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Web.Generics.HtmlHelpers
{
    public interface IWebGrid : IWebSortable, IWebPager
    {
        IEnumerable GetDataSourceEnumerator();
        //IEnumerable<WebProperty> GetPropertyNames();
        Object GetValue(object item, String propertyName);
    }
}
