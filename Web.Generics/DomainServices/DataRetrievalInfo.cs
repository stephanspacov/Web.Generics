using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Web.Generics.DomainServices
{
    public class DataRetrievalInfo<T> where T : class
    {
        public Expression<Func<T, Boolean>> Filter { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public SortingInfo SortingInfo { get; set; }
        public Int32 TotalItemCount { get; set; }
    }
}
