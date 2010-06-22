using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.Routing;
using MvcContrib.ActionResults;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Web;

namespace Web.Generics
{
	public class GenericController<T> : Controller
	{   
        protected static List<String> ValidFormats = new List<String> { "html", "json", "xml", "xls", "jqgrid" };
		protected string Format { get; set; }

		private const string FormatKey = "format";

		protected ActionResult FormatResult(object model)
		{
			switch (Format)
			{
				case "html":
					return View(model);
				case "json":
                    return Content(SerializeAsJson(model, true));
				case "xml":
					//return new XmlResult(model);
				case "xls":
				default:
					throw new FormatException(
						string.Format("The format \"{0}\" is invalid", Format));
			}
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
			ExtractRequestedFormat(filterContext.RouteData.Values);
		}

		private void ExtractRequestedFormat(RouteValueDictionary routeValues)
		{
			if (routeValues.ContainsKey(FormatKey))
			{
				var requestedFormat = routeValues[FormatKey].ToString().ToLower();

				if (routeValues.ContainsKey("id") && 
                    ValidFormats.Contains(routeValues["id"].ToString().ToLower()))
                {
					requestedFormat = routeValues["id"].ToString().ToLower();
				}

				if (ValidFormats.Contains(requestedFormat))
				{
					Format = requestedFormat;
					return;
				}
			}
			Format = "html";
		}

		private GenericService<T> genericService;
		public GenericController(GenericService<T> genericService)
		{
			this.genericService = genericService;
		}

		//
		// GET /GenericArea/T/Index
		virtual public ActionResult Index()
		{
			IList<T> results = this.genericService.Select();
			return FormatResult(results);
		}

        [HttpPost]
        virtual public ActionResult FilterParams(IList<String> Properties, IList<String> Comparers, IList<String> Values)
        {
            List<FilterCondition> conditions = new List<FilterCondition>();

            for (int i = 0; i < Properties.Count; i++)
            {               
                FilterCondition condition = new FilterCondition();
                condition.Property = Properties[i];
                condition.Comparer = (FilterCondition.ComparerType)Enum.
                    Parse(typeof(FilterCondition.ComparerType), Comparers[i]);
                condition.Value = Values[i];
                conditions.Add(condition);
            }

            return this.Content(SerializeAsJson(conditions, false));
        }

        private static string SerializeAsJson(Object obj, Boolean indent)
        {
            using (StringWriter streamWriter = new StringWriter())
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                if (indent)
                    jsonWriter.Formatting = Formatting.Indented;
                JsonSerializer serializer = new JsonSerializer
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new NHibernateContractResolver(),
                };
                serializer.Serialize(jsonWriter, obj);

                return streamWriter.ToString();
            }
        }

        private static IList<TList> DeserializeFromJson<TList>(string obj)
        {
            using (StringReader reader = new StringReader(obj))
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (IList<TList>)serializer.Deserialize(jsonReader, typeof(IList<TList>));
            }
        }
        
        virtual public ActionResult 
            JqGridFilter(String rows, String page, String sidx, String sord, String conditions)
        {
            FilterParameters filterParameters = new FilterParameters(page, rows, sord, sidx, null);

            if (conditions != null)
            {
                filterParameters.FilterConditions = DeserializeFromJson<FilterCondition>(conditions);
            }

            IList<T> results = this.genericService.Select(filterParameters);
            Int32 resultsTotalCount = this.genericService.Count(filterParameters);
            Int32 resultsTotalPages = (resultsTotalCount / filterParameters.PageSize);
            resultsTotalPages += (resultsTotalCount % filterParameters.PageSize) > 0 ? 1 : 0;

            jqGrid.jqGridResult result = new jqGrid.jqGridResult();
            result.Page = filterParameters.PageIndex;
            result.Records = resultsTotalCount;
            result.Rows = results;
            result.Total = resultsTotalPages;

            Regex dateRegex = new Regex(@"\\/Date\((.*)([-|+])(.*)\)\\/");
            string jsonstring = dateRegex.Replace(SerializeAsJson(result, true), new MatchEvaluator(DateTimeReplacer));

            return this.Content(jsonstring);
        }

        private string DateTimeReplacer(Match match)
        {
            DateTime baseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).
                AddMilliseconds(double.Parse(match.Groups[1].Value));
            if (match.Groups[2].Value == "+")
            {
                baseDateTime = baseDateTime.AddHours(double.Parse(match.Groups[3].Value)/100);
            }
            else
            {
                baseDateTime = baseDateTime.AddHours(- double.Parse(match.Groups[3].Value)/100);
            }
            return baseDateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

		//
		// GET: /GenericArea/Test/Details/5
		virtual public ActionResult Details(Int32 id)
		{
			T obj = this.genericService.SelectById(id);
			return FormatResult(obj);
		}

		//
		// GET: /GenericArea/Test/Create
		virtual public ActionResult Create()
		{
			return View();
		}

		//
		// POST: /GenericArea/Test/Create
        [HttpPost]
        virtual public ActionResult Create(GenericViewModel<T> obj)
        {
            if (ModelState.IsValid)
            {
                this.genericService.Insert(obj.Instance);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

		//
		// GET: /GenericArea/Test/Delete/5
		virtual public ActionResult Delete(int id)
		{
			return Details(id);
		}

		//
		// POST: /GenericArea/Test/Delete/5
		[HttpPost]
		virtual public ActionResult Delete(T obj)
		{
				this.genericService.Delete(obj);
				return RedirectToAction("Index");
		}

		//
		// GET: /GenericArea/Test/Edit/5
		virtual public ActionResult Edit(int id)
		{
			return Details(id);
		}

		//
		// POST: /GenericArea/Test/Edit/5
		[HttpPost]
		virtual public ActionResult Edit(GenericViewModel<T> obj)
		{
            if (ModelState.IsValid)
            {
                this.genericService.Update(obj.Instance);
                return RedirectToAction("Index");
            }

            return View(obj);
		}

        // GET: /GenericArea/Test/EditModal/5
        virtual public ActionResult EditModal(int id)
        {
            T obj = this.genericService.SelectById(id);
            return View("Edit", "FormModal", obj);
        }

        // Post: Upload File
        virtual public string UploadFile(HttpPostedFileBase FileData) 
        {
            string fileDir = Request.MapPath("~/Files/" + typeof(T).Name + "/");
            string fileName = DateTime.Now.ToString("ddMMyyhhss") + FileData.FileName;
            if (!System.IO.Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            FileData.SaveAs(fileDir + fileName);

            return "~/Files/" + typeof(T).Name + "/" + fileName;
        }
	}
}