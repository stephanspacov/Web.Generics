using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace Web.Generics.HtmlHelpers
{
    public class WebGrid<T> : IWebGrid<T>
    {
        public WebGrid()
        {
            this.AllowPaging = true;
            this.AllowSorting = true;
            this.PageSize = 2;
            this.PageIndex = 1;
            this.SortOrder = SortOrder.Ascending;
        }

        public IList<T> DataSource { get; set; }
        public IEnumerable GetDataSourceEnumerator()
        {
            return (IEnumerable)DataSource;
        }

        public IList<FilterCondition> FilterConditions { get; set; }
        public string SearchQuery { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public bool AllowPaging { get; set; }        
        public int NumberOfPages {
            get
            {
                if (PageSize == 0 || TotalItemCount == 0) return 1;
                return (int)Math.Ceiling((float)TotalItemCount / (float)PageSize);
            }
        }
        public int PageStartIndex {
            get {
                return PageSize * (PageIndex - 1);
            }
        }
        public bool HasPrevious {
            get {
                return this.PageIndex > 1;
            }
        }
        public bool HasNext
        {
            get {
                return this.PageIndex != this.NumberOfPages;
            }
        }

        public string SortProperty { get; set; }
        public SortOrder SortOrder { get; set; }
        public bool AllowSorting { get; set; }

        public void DefineSortProperty<TModel, TValue>(System.Linq.Expressions.Expression<Func<TModel, TValue>> expression)
        {
            this.SortProperty = expression.Type.ToString();
        }

        public IEnumerable<WebProperty> GetPropertyNames()
        {
            foreach (var property in typeof(T).GetProperties())
            {
                var caption = property.Name;
                Object[] attrs = property.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                if (attrs.Length > 0) {
                    caption = ((DisplayNameAttribute)attrs[0]).DisplayName;
                }
                yield return new WebProperty { Name = property.Name, Caption = caption };
            }
        }

        public object GetValue(object item, string propertyName) {
            if (item == null) return null;

            return item.GetType().GetProperty(propertyName).GetValue(item, null);
        }
    }
}
