using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Routing;
namespace Web.Generics.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class DropDownListAttribute : Attribute
    {
        private static string defaultTemplateName;

        public DropDownListAttribute(string viewDataKey, string dataValueField)
            : this(viewDataKey, dataValueField, null)
        {
        }

        public DropDownListAttribute(string viewDataKey, string dataValueField, string dataTextField)
            : this(viewDataKey, dataValueField, dataTextField, null)
        {
        }

        public DropDownListAttribute(string viewDataKey, string dataValueField, string dataTextField, string optionLabel)
            : this(DefaultTemplateName, viewDataKey, dataValueField, dataTextField, optionLabel, null)
        {
        }

        public DropDownListAttribute(string viewDataKey, string dataValueField, string dataTextField, string optionLabel, object htmlAttributes)
            : this(DefaultTemplateName, viewDataKey, dataValueField, dataTextField, optionLabel, htmlAttributes)
        {
        }

        public DropDownListAttribute(string templateName, string viewDataKey, string dataValueField, string dataTextField, string optionLabel, object htmlAttributes)
        {
            if (string.IsNullOrEmpty(templateName))
            {
                throw new ArgumentException("Template name cannot be empty.");
            }

            if (string.IsNullOrEmpty(viewDataKey))
            {
                throw new ArgumentException("View data key cannot be empty.");
            }

            if (string.IsNullOrEmpty(dataValueField))
            {
                throw new ArgumentException("Data value field cannot be empty.");
            }

            TemplateName = templateName;
            ViewDataKey = viewDataKey;
            DataValueField = dataValueField;
            DataTextField = dataTextField;
            OptionLabel = optionLabel;
            HtmlAttributes = new RouteValueDictionary(htmlAttributes);
        }

        public static string DefaultTemplateName
        {
            get
            {
                if (string.IsNullOrEmpty(defaultTemplateName))
                {
                    defaultTemplateName = "DropDownList";
                }

                return defaultTemplateName;
            }
            set
            {
                defaultTemplateName = value;
            }
        }

        public string TemplateName { get; private set; }

        public string ViewDataKey { get; private set; }

        public string DataValueField { get; private set; }

        public string DataTextField { get; private set; }

        public string OptionLabel { get; private set; }

        public IDictionary<string, object> HtmlAttributes { get; private set; }

        public object GetSelectedValue(object model)
        {
            return GetPropertyValue(model, DataValueField);
        }

        public object GetSelectedText(object model)
        {
            return GetPropertyValue(model, !string.IsNullOrEmpty(DataTextField) ? DataTextField : DataValueField);
        }

        private static object GetPropertyValue(object model, string propertyName)
        {
            if (model != null)
            {
                PropertyDescriptor property = GetTypeDescriptor(model.GetType()).GetProperties()
                                                                                .Cast<PropertyDescriptor>()
                                                                                .SingleOrDefault(p => string.Compare(p.Name, propertyName, StringComparison.OrdinalIgnoreCase) == 0);

                if (property != null)
                {
                    return property.GetValue(model);
                }
            }

            return null;
        }

        private static ICustomTypeDescriptor GetTypeDescriptor(Type type)
        {
            return new AssociatedMetadataTypeTypeDescriptionProvider(type).GetTypeDescriptor(type);
        }
    }
}