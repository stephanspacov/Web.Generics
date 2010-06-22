using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.jqGrid;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Web.Generics.HtmlHelpers
{
    public static class jqGridHelper
    {
        //Implementar para o SharedView Index
        public static string jqGridForModel(this HtmlHelper helper)
        {
            return "";
        }

        /// <summary>
        /// Create a jqGrid for Model T with all columns and default options.
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string jqGridFor<T>(this HtmlHelper helper)
        {
            return jqGridFor<T>(helper, new string[] { }, typeof(T).Name);
        }

        /// <summary>
        /// Create a jqGrid named 'gridName' for Model T for properties 'colNames'
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="helper"></param>
        /// <param name="colNames"></param>
        /// <param name="gridName"></param>
        /// <returns></returns>
        public static string jqGridFor<T>(this HtmlHelper helper, string[] colNames, string gridName)
        {
            jqGridOptions options = CreateDefaultOptions<T>(gridName);

            GenerateColNamesAndColModels<T>(colNames, options);

            return jqGridFor<T>(helper, options);
        }

        /// <summary>
        /// Create a jqGrid for Model T with given 'options'
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="helper"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string jqGridFor<T>(this HtmlHelper helper, jqGridOptions options)
        {
            string filters = CreateFilters(options);

            return jqGridTemplate.GetDivGridTemplate().
                Replace("{GRIDNAME}", options.Name).
                Replace("{URL}", options.Url).
                Replace("{ID}", options.Id).
                Replace("{FILTERS}", filters).
                Replace("{COLNAMES}", JsonSerializeString(options.ColNames)).
                Replace("{COLMODEL}", JsonSerializeString(options.ColModel));
        }

        private static string CreateFilters(jqGridOptions options)
        {
            string filters = string.Empty;

            int i = 0;
            foreach (jqGridFilter filter in options.Filters)
            {
                switch (filter.ControlType)
                {
                    case ControlType.Text:
                        jqGridTemplate.AppendTextFilter(i, filter, ref filters);
                        break;
                    case ControlType.DatePicker:
                        jqGridTemplate.AppendDatePickerFilter(i, filter, ref filters);
                        break;
                    case ControlType.Select:
                        jqGridTemplate.AppendSelectFilter(i, filter, filter.FKCollection, ref filters);
                        break;
                    case ControlType.CheckBox:
                        jqGridTemplate.AppendCheckBoxFilter(i, filter, ref filters);
                        break;
                }
                i++;
            }
            return filters;
        }

        private static void GenerateColNamesAndColModels<T>(string[] colNames, jqGridOptions options)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                // se a propriedade não está na lista de colunas a serem 
                // incluidas no grid, passa para a próxima
                if (!colNames.Contains<string>(property.Name) && colNames.Count<string>() > 0)
                    continue;

                jqGridColModel colModel = new jqGridColModel();
                colModel.name = property.Name;
                colModel.align = "center";
                colModel.sortable = true;
                colModel.width = null;

                jqGridFilter filter = new jqGridFilter();
                filter.Property = property.Name;
                filter.Value = string.Empty;

                GenerateOptionsAndFilters(options, property, colModel, filter);
            }
        }

        private static void GenerateOptionsAndFilters(jqGridOptions options, PropertyInfo property, jqGridColModel colModel, jqGridFilter filter)
        {
            //IList
            if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
            {
                GenerateOptionsAndFilterForList(property, colModel, filter);
            }
            //Int
            else if (property.PropertyType == typeof(Int32) ||
                property.PropertyType == typeof(Int32?))
            {
                GenerateOptionsAndFilterForInt(options, property, colModel, filter);
            }
            //DateTime
            else if (property.PropertyType == typeof(DateTime) ||
                property.PropertyType == typeof(DateTime?))
            {
                GenerateOptionsAndFilterForDateTime(options, property, colModel, filter);

            }
            //Boolean
            else if (property.PropertyType == typeof(Boolean) ||
                property.PropertyType == typeof(Boolean?))
            {
                GenerateOptionsAndFilterForBoolean(options, property, colModel, filter);
            }
            //String
            else if (property.PropertyType == typeof(String))
            {
                GenerateOptionsAndFilterForString(options, property, colModel, filter);
            }
            //Other (Objects)
            else
            {
                GenerateOptionsAndFilterForOtherObjectTypes(options, property, colModel, filter);
            }
        }

        private static void GenerateOptionsAndFilterForOtherObjectTypes(jqGridOptions options, PropertyInfo property, jqGridColModel colModel, jqGridFilter filter)
        {
            options.ColNames.Add(property.Name);

            filter.ControlType = ControlType.Select;
            filter.Comparer = FilterCondition.ComparerType.eq;
            colModel.index = property.Name;

            String displayPropertyName = null;

            Object[] attributes = property.PropertyType.GetCustomAttributes(typeof(DisplayColumnAttribute), false);
            if (attributes.Length == 1)
            {
                DisplayColumnAttribute displayColumn = (DisplayColumnAttribute)attributes[0];
                displayPropertyName = displayColumn.DisplayColumn;
            }
            else
            {
                PropertyInfo stringProperty = property.PropertyType.GetProperties().FirstOrDefault(x => x.PropertyType == typeof(String));
                if (stringProperty != null) displayPropertyName = stringProperty.Name;
            }

            if (displayPropertyName != null)
            {
                colModel.index += "." + displayPropertyName;
            }
            colModel.name = colModel.index;

            options.Filters.Add(filter);
            options.ColModel.Add(colModel);
        }

        private static void GenerateOptionsAndFilterForString(jqGridOptions options, PropertyInfo property, jqGridColModel colModel, jqGridFilter filter)
        {
            options.ColNames.Add(property.Name);

            filter.ControlType = ControlType.Text;
            filter.Comparer = FilterCondition.ComparerType.slike;
            colModel.index = property.Name;

            options.Filters.Add(filter);
            options.ColModel.Add(colModel);
        }

        private static void GenerateOptionsAndFilterForBoolean(jqGridOptions options, PropertyInfo property, jqGridColModel colModel, jqGridFilter filter)
        {
            options.ColNames.Add(property.Name);

            filter.ControlType = ControlType.CheckBox;
            colModel.formatter = "checkbox";
            colModel.index = property.Name;

            options.Filters.Add(filter);
            options.ColModel.Add(colModel);
        }

        private static void GenerateOptionsAndFilterForDateTime(jqGridOptions options, PropertyInfo property, jqGridColModel colModel, jqGridFilter filter)
        {
            options.ColNames.Add(property.Name);

            filter.ControlType = ControlType.DatePicker;
            filter.Comparer = FilterCondition.ComparerType.eq;
            colModel.sorttype = "date";
            colModel.formatoptions = new jqGridFormatOptions();
            colModel.formatter = "date";
            colModel.formatoptions.srcformat = "d/m/Y h:i:s";
            colModel.formatoptions.newformat = "d/m/Y";
            colModel.index = property.Name;

            options.Filters.Add(filter);
            options.ColModel.Add(colModel);
        }

        private static void GenerateOptionsAndFilterForInt(jqGridOptions options, PropertyInfo property, jqGridColModel colModel, jqGridFilter filter)
        {
            options.ColNames.Add(property.Name);

            filter.ControlType = ControlType.Text;
            filter.Comparer = FilterCondition.ComparerType.eq;
            colModel.sorttype = "int";

            colModel.index = property.Name;

            options.Filters.Add(filter);
            options.ColModel.Add(colModel);
        }

        private static void GenerateOptionsAndFilterForList(PropertyInfo property, jqGridColModel colModel, jqGridFilter filter)
        {
            filter.ControlType = ControlType.Text;
            filter.Comparer = FilterCondition.ComparerType.eq;
            //filter.FKCollection = new GenericService<
            colModel.index = property.Name;
            colModel.sortable = false;
        }
        
        private static string JsonSerializeString(object obj)
        {
            using (StringWriter streamWriter = new StringWriter())
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                jsonWriter.Formatting = Formatting.None;
                JsonSerializer serializer = new JsonSerializer
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Error,
                    ContractResolver = new NHibernateContractResolver(),
                };
                serializer.Serialize(jsonWriter, obj);
                return streamWriter.ToString();
            }
        }

        private static jqGridOptions CreateDefaultOptions<T>(string gridName)
        {
            jqGridOptions options = new jqGridOptions();

            options.Name = gridName;
            options.Url = "/Admin/" + typeof(T).Name + "/";
            options.DataType = "json";
            options.HideGrid = "false";
            options.Heigth = "100%";
            options.AltRows = "true";
            options.AutoWidth = "true";
            options.Id = "Id";

            return options;
        }

    }
}
