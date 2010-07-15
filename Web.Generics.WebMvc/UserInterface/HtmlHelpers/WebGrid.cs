using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Web.Generics.HtmlHelpers
{
    public class WebGrid<T> : IWebGrid<T>
    {
        public WebGrid()
        {
            //this.FilterConditions = new List<FilterCondition>();
            this.AllowPaging = true;
            this.AllowSorting = true;
            this.PageSize = 10;
            this.PageIndex = 1;
            this.DataSource = new List<T>();
            //this.SortOrder = SortOrder.Ascending;
        }

        public IEnumerable<T> DataSource { get; set; }
        public IEnumerable GetDataSourceEnumerator()
        {
            return (IEnumerable)DataSource;
        }

        //public IList<FilterCondition> FilterConditions { get; set; }
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
        public string PreviousSortProperty { get; set; }
        public SortOrder SortOrder { get; set; }
        public SortOrder PreviousSortOrder { get; set; }
        public bool AllowSorting { get; set; }

        public void CorrectSortPropertyAndOrder()
        {
            var viewModel = this;
            if (String.IsNullOrEmpty(viewModel.SortProperty))
            {
                viewModel.SortProperty = viewModel.PreviousSortProperty;
                viewModel.SortOrder = viewModel.PreviousSortOrder;
            }
            else
            {
                if (viewModel.PreviousSortProperty == viewModel.SortProperty && viewModel.PreviousSortOrder == SortOrder.Ascending)
                {
                    viewModel.SortOrder = SortOrder.Descending;
                }
                else
                {
                    viewModel.SortOrder = SortOrder.Ascending;
                }
            }
        }

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

            var propertyInfo = item.GetType().GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new Exception(String.Format("Propriedade {0} não existe na entidade {1}", propertyName, item.GetType()));
            }

            return propertyInfo.GetValue(item, null);
        }

        internal Expression<Func<T, Boolean>> GetFilterExpression()
        {
            return null;
        }


        Expression<Func<T, bool>> IWebGrid<T>.GetFilterExpression()
        {
            throw new NotImplementedException();
        }
    }
}
