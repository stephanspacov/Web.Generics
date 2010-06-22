using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Web.Generics
{
	public class ViewModelMaster
	{
		public String SearchQuery { get; set; }

		public Int32? PageIndex { get; set; }
		public Int32? PageSize { get; set; }
		public Int32? NumberOfPages { get; set; }
		public Int32? Total { get; set; }
		public String SortProperty { get; set; }
		public Boolean? SortOrder { get; set; }

        public String PreviousSortProperty { get; set; }
        public Boolean? PreviousSortOrder { get; set; }


        public SelectList PageSizes { get; set; }

		public Boolean ShowFilter { get; set; }

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

	}
}
