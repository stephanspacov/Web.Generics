using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace Web.Generics
{
    public class GenericViewModel<T> : FilterParameters
	{
        public GenericViewModel()
        {
            this.Instance = Activator.CreateInstance<T>();
            this.InstanceList = Activator.CreateInstance<List<T>>();
            this.DeletedItems = new List<Int32>();
            this.SelectLists = new Dictionary<String, SelectList>();
            this.SelectListValues = new Dictionary<String, Object>();
        }

		virtual public T Instance { get; set; }
		virtual public IList<T> InstanceList { get; set; }

        virtual public IDictionary<String, SelectList> SelectLists { get; private set; }
        virtual public IDictionary<String, Object> SelectListValues { get; private set; }

        virtual public IList<Int32> DeletedItems { get; set; }

        public List<PropertyInfo> Properties
        {
            get
            {
                List<PropertyInfo> props = new List<PropertyInfo>();
                PropertyInfo[] propertyArray = this.Instance.GetType().GetProperties();
                foreach (PropertyInfo propertyInfo in propertyArray)
                {
                    props.Add(propertyInfo);
                }
                return props;
            }
        }

        public void GetCorrectValuesForFilter(int pageNumber, int numberOfResults, string goTo, string sortProperty)
        {
            this.PageIndex = 1;

            if (pageNumber > 1)
            {
                this.PageIndex = pageNumber;
            }

            if (this.PageSize == 0) this.PageSize = 10;

            //this.NumberOfPages = (int)Math.Ceiling((double)numberOfResults / this.PageSize);

            // if the user has cliked on first, last or goTo button
            if (!String.IsNullOrEmpty(goTo))
            {
                // if first was cliked
                if (goTo.Equals("goto"))
                {
                    this.PageIndex = 1;
                    if (pageNumber > 0) this.PageIndex =pageNumber;
                    if (this.PageIndex > this.NumberOfPages)
                        this.PageIndex = this.NumberOfPages;
                    else
                        if (this.PageIndex < 1)
                            this.PageIndex = 1;
                }
                else
                {
                    // if first was cliked
                    if (goTo.Equals("first"))
                        this.PageIndex = 1;
                    else
                        // if last was cliked
                        if (goTo.Equals("last"))
                            this.PageIndex = this.NumberOfPages;
                }
            }

//            this.SortOrder = this.SortOrder ?? true;
            /*
            if (!String.IsNullOrEmpty(sortProperty))
            {
                if (this.SortOrder == SortOrderEnum.Ascending)
                {
                    this.SortOrder = SortOrderEnum.Descending;
                }
                this.SortProperty = sortProperty;
            }*/

            this.Total = numberOfResults;
            this.PageSizes = new SelectList(new[] { "5", "10", "20", "50" }, "10");

            //ShowFilter = true;
        }
    }
}
