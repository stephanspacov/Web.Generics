using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;

namespace Web.Generics
{
    public class FilterParameters
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
    }
}
