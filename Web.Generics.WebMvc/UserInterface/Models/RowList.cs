using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.WebMvc.UserInterface.HtmlHelpers;
using Web.Generics.DomainServices;

namespace Web.Generics.UserInterface.Models
{
	public class RowList
	{
        public RowList()
        {
            this.PagingInfo = new PagingInfo();
            this.SortingInfo = new SortingInfo();
        }

        public PagingInfo PagingInfo { get; set; }
        public SortingInfo SortingInfo { get; set; }
	}
}
