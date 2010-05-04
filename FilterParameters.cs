using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;
using Web.Generics.HtmlHelpers;

namespace Web.Generics
{
    public class FilterParameters : IWebGrid
    {
        public enum SortOrderEnum
        {
            Ascending, Descending
        }

        public FilterParameters()
        {
            this.FilterConditions = new List<FilterCondition>();
            this.FieldsToSearch = new List<String>();
            this.PageSize = 10;
            this.PageIndex = 1;
            this.AllowPaging = true;
        }

        public FilterParameters(String page, String rows, String sord, String sidx, IList<FilterCondition> conditions)
        {
            this.PageIndex = Int32.Parse(page);
            this.PageSize = Int32.Parse(rows);
            this.SortOrder = sord == "asc" ? SortOrderEnum.Ascending : SortOrderEnum.Descending;
            this.SortProperty = sidx;
            this.FilterConditions = conditions;
        }

        public Boolean HasPrevious
        {
            get
            {
                return this.PageIndex > 1;
            }
        }

        public Boolean HasNext
        {
            get
            {
                return this.PageIndex < this.NumberOfPages;
            }
        }

        public Boolean AllowPaging { get; set; }
        public Boolean AllowSorting { get; set; }
        public SelectList PageSizes { get; set; }        
        public int Total { get; set; }
        public int NumberOfPages {
            get
            {
                if (PageSize == 0 || Total == 0) return 1;
                return (int)Math.Ceiling((float)Total / (float)PageSize);
            }
        }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int PageStartIndex { get { return PageSize * (PageIndex - 1); } }
        public string SortProperty { get; set; }
        public SortOrderEnum SortOrder { get; set; }
        public string PreviousSortProperty { get; set; }
        public SortOrderEnum PreviousSortOrder { get; set; }
        public int NextPageIndex { get; set; }
        public IList<FilterCondition> FilterConditions { get; set; }

        [DisplayName("Palavra-chave")]
        public String SearchQuery { get; set; }
        public IList<String> FieldsToSearch { get; set; }
        public Boolean IsAscending {
            get
            {
                return SortOrder == SortOrderEnum.Ascending;
            }
        }

        public void AddCondition(String property, Object value)
        {
            this.AddCondition(property, FilterCondition.ComparerType.eq, value);
        }

        public void AddCondition(String property, FilterCondition.ComparerType comparer, Object value)
        {
            this.FilterConditions.Add(new FilterCondition { Property = property, Comparer = comparer, Value = value });
        }

        public System.Collections.IEnumerable GetDataSourceEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebProperty> GetPropertyNames()
        {
            throw new NotImplementedException();
        }

        public object GetValue(object item, string propertyName)
        {
            throw new NotImplementedException();
        }

        public void DefineSortProperty<TModel, TValue>(System.Linq.Expressions.Expression<Func<TModel, TValue>> expression)
        {
            throw new NotImplementedException();
        }

        SortOrder IWebSortable.SortOrder
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool SortingEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public int TotalItemCount
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool PagingEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
