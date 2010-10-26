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
using System.Collections;

namespace Web.Generics.UserInterface.JqGrid
{
    public enum ControlType
    {
        Text,
        Select,
        DatePicker,
        CheckBox
    }

    public class JqGridFilter
    {
        public string Property { get; set; }
        public FilterCondition.ComparerType Comparer { get; set; }
        public string Value { get; set; }
        public ControlType ControlType { get; set; }
        public IList FKCollection { get; set; }
    }

    public class JqGridColModel
    {
        public string name { get; set; }
        public string index { get; set; }
        public bool? sortable { get; set; }
        public string sorttype { get; set; }
        public int? width { get; set; }
        public string align { get; set; }
        public string formatter { get; set; }
        public JqGridFormatOptions formatoptions { get; set; }
    }

    public class JqGridFormatOptions
    {
        public string srcformat { get; set; }
        public string newformat { get; set; }
    }

    public class JqGridOptions
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Heigth { get; set; }
        public string Width { get; set; }
        public string DataType { get; set; }
        public string HideGrid { get; set; }
        public string AltRows { get; set; }
        public string AutoWidth { get; set; }
        public string Id { get; set; }
        public IList<String> ColNames { get; set; }
        public IList<JqGridColModel> ColModel { get; set; }
        public IList<JqGridFilter> Filters { get; set; }

        public JqGridOptions()
        {
            this.ColNames = new List<String>();
            this.ColModel = new List<JqGridColModel>();
            this.Filters = new List<JqGridFilter>();

            this.ColNames.Add("Actions");
            JqGridColModel actions = new JqGridColModel();
            actions.name = "Actions";
            actions.index = "Actions";
            actions.sortable = false;
            actions.width = 1;
            this.ColModel.Add(actions);
        }

        //Tentar modificar o formato json que o JqGrid interpreta (http://www.trirand.com/JqGridwiki/doku.php?id=wiki:retrieving_data#json_data)

    }
}
