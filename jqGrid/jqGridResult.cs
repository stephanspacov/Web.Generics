using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Web.Generics.jqGrid
{
    public class jqGridResult
    {
        public int Page {get;set;}
        public int Total { get; set; }
        public int Records { get; set; }
        public IEnumerable Rows { get; set; }
    }
}
