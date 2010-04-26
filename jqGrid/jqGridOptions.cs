using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Web.Generics.jqGrid
{
    public enum ControlType
    {
        Text,
        Select,
        DatePicker,
        CheckBox
    }

    public class jqGridFilter
    {
        public string Property { get; set; }
        public FilterCondition.ComparerType Comparer { get; set; }
        public string Value { get; set; }
        public ControlType ControlType { get; set; }
        public IList FKCollection { get; set; }
    }

    public class jqGridColModel
    {
        public string name { get; set; }
        public string index { get; set; }
        public bool? sortable { get; set; }
        public string sorttype { get; set; }
        public int? width { get; set; }
        public string align { get; set; }
        public string formatter { get; set; }
        public jqGridFormatOptions formatoptions { get; set; }
    }

    public class jqGridFormatOptions
    {
        public string srcformat { get; set; }
        public string newformat { get; set; }
    }

    public class jqGridOptions
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
        public IList<jqGridColModel> ColModel { get; set; }
        public IList<jqGridFilter> Filters { get; set; }

        public jqGridOptions()
        {
            this.ColNames = new List<String>();
            this.ColModel = new List<jqGridColModel>();
            this.Filters = new List<jqGridFilter>();

            this.ColNames.Add("Actions");
            jqGridColModel actions = new jqGridColModel();
            actions.name = "Actions";
            actions.index = "Actions";
            actions.sortable = false;
            actions.width = 1;
            this.ColModel.Add(actions);
        }

        //Tentar modificar o formato json que o jqgrid interpreta (http://www.trirand.com/jqgridwiki/doku.php?id=wiki:retrieving_data#json_data)

    }
}
