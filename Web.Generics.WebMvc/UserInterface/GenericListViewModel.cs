using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.WebMvc.UserInterface
{
    public class GenericListViewModel
    {
        public Column[] Columns { get; set; }
        public Row[] Rows { get; set; }

        public class Column
        {
            public String HeaderText { get; set; }
            public String PropertyName { get; set; }
        }

        public class Row
        {
            public Cell[] Cells { get; set; }
        }

        public class Cell
        {
            public Column Column { get; set; }
            public Row Row { get; set; }
            public String Value { get; set; }
        }
    }
}
