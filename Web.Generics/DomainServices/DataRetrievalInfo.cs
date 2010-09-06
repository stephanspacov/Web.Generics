using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Web.Generics.UserInterface.HtmlHelpers;

namespace Web.Generics.DomainServices
{
    public class DataRetrievalInfo<T>
    {
        public Expression<Func<T, Boolean>> Filter { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public SortingInfo<T> SortingInfo { get; set; }
        public Int32 TotalItemCount { get; set; }
    }
}
