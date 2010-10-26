using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.DomainServices
{
    public interface IRowList
    {
        PagingInfo PagingInfo { get; set; }
        SortingInfo SortingInfo { get; set; }
    }
}
