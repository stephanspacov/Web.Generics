/*
Copyright 2010 Inspira Tecnologia.
All Rights Reserved.

Contact: Thiago Alves <thiago.alves@inspira.com.br>

This file is part of Web.Generics

Web.Generics is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Web.Generics is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with Web.Generics.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
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
