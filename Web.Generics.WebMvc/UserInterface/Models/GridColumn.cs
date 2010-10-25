using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.UserInterface.Models
{
    public class GridColumn
    {
		public String HeaderText { get; set; }
		public String PropertyName { get; set; }

        public void Add(String propertyName, String headerText)
        {
            this.PropertyName = propertyName;
            this.HeaderText = headerText;
        }

        public static IList<GridColumn> Create(params String[] propertyTextPairs)
        {
            if (propertyTextPairs.Length % 2 == 1) throw new ArgumentOutOfRangeException("A quantidade de parâmetros deve ser um número par");

            IList<GridColumn> grid = new List<GridColumn>();
            int i = 1;

            GridColumn column = null;
            foreach (string parameter in propertyTextPairs)
            {
                if (i > 0)
                {
                    column = new GridColumn();
                    column.PropertyName = parameter;   
                }
                if (i < 0)
                {
                    column.HeaderText = parameter;
                    grid.Add(column);
                }
                i = i * -1;
            }

            return grid;
        }
    }
}
