﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.DomainServices
{
    public partial class PagingInfo
    {
		public PagingInfo()
		{
			this.PageSize = 10;
            this.PageIndex = 1;
            this.PagingEnabled = false;
            this.ShowPageNumbers = true;
		}

        public Boolean PagingEnabled { get; set; }
        public Boolean ShowPageNumbers { get; set; }
        public Int32 TotalItemCount { get; set; }
        public Int32 PageSize { get; set; }
        public Int32 PageIndex { get; set; }
        public Int32 PreviousPageIndex { get; set; }

        public Int32 GetPageIndex()
        {
            return PageIndex;
        }

		public Boolean HasPrevious
		{
			get
			{
				return (PageIndex > 1);
			}
		}

		public Boolean HasNext
		{
			get
			{
				return (PageIndex < NumberOfPages);
			}
		}

		public Int32 NumberOfPages
		{
			get
			{
				return (Int32)Math.Ceiling((Double)TotalItemCount / (Double)PageSize);
			}
        }
	}
}
