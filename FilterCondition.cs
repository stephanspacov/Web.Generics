using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics
{
    public class FilterCondition
    {
        public enum ComparerType
        {
            eq,
            neq,
            gt,
            ge,
            lt,
            le,
            like,
            slike,
            elike
        }

        public string Property { get; set; }
        public ComparerType Comparer { get; set; }
        public Object Value { get; set; }
    }
}
