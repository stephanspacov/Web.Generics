using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.Routing;
using MvcContrib.ActionResults;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Web;
using Microsoft.Practices.Unity;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using Web.Generics.HtmlHelpers;

namespace Web.Generics
{
    public class GenericViewModelController<TModel, TViewModel> : Controller where TViewModel : GenericViewModel<TModel>
    {
        protected static List<String> ValidFormats = new List<String> { "html", "json", "xml", "xls", "jqgrid" };
        protected string Format { get; set; }

        private const string FormatKey = "format";
        protected ActionResult FormatResult(TViewModel model)
        {
            return FormatResult(model, null);
        }
        protected ActionResult FormatResult(TViewModel model, String view)
        {
            switch (Format)
            {
                case "html":
                    return View(view, model);
                case "json":
                    return Content(SerializeAsJson(model.InstanceList, true));
                case "xml":
                //                    return new XmlResult(model.Instance);
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

        private GenericService<TModel> genericService;
        public GenericViewModelController(GenericService<TModel> genericService)
        {
            this.genericService = genericService;
        }

        public ActionResult JqGrid()
        {
            TViewModel viewModel = (TViewModel)Activator.CreateInstance(typeof(TViewModel));
            viewModel.InstanceList = this.genericService.Select();
            return FormatResult(viewModel);
        }

        [AcceptVerbs("GET")]
        virtual public ActionResult Index()
        {
            TViewModel viewModel = Activator.CreateInstance<TViewModel>();
            if (Session[viewModel.GetType().ToString()] != null)
            {
                viewModel = (TViewModel)Session[viewModel.GetType().ToString()];
            }
            var grid = viewModel.DefaultGrid;

            grid.DataSource = this.genericService.Select(grid);
            grid.TotalItemCount = this.genericService.Count(grid);

            PopulateDropDowns(viewModel);

            return View(viewModel);
        }

        [AcceptVerbs("POST")]
        virtual public ActionResult Index(TViewModel viewModel)
        {
            var grid = viewModel.DefaultGrid;

            CreateDropDownFilters(viewModel);

            grid.DataSource = this.genericService.Select(grid);
            grid.TotalItemCount = this.genericService.Count(grid);

            PopulateDropDowns(viewModel);

            Session.Add(viewModel.GetType().ToString(), viewModel);
            return FormatResult(viewModel);
        }

        private void CreateDropDownFilters(TViewModel viewModel)
        {
            foreach (String key in viewModel.SelectListValues.Keys)
            {
                Object value = viewModel.SelectListValues[key];
                viewModel.DefaultGrid.FilterConditions.Add(new FilterCondition
                {
                    Comparer = FilterCondition.ComparerType.eq,
                    Property = key + ".ID",
                    Value = value
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        virtual public ActionResult FilterParams(IList<String> Properties, IList<String> Comparers, IList<String> Values)
        {
            List<FilterCondition> conditions = new List<FilterCondition>();

            for (int i = 0; i < Properties.Count; i++)
            {
                FilterCondition condition = new FilterCondition();
                condition.Property = Properties[i];
                condition.Comparer = (FilterCondition.ComparerType)Enum.Parse(typeof(FilterCondition.ComparerType), Comparers[i]);
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

        virtual public ActionResult JqGridFilter(String rows, String page, String sidx, String sord, String conditions)
        {
            FilterParameters filterParameters = new FilterParameters(page, rows, sord, sidx, null);

            if (conditions != null)
            {
                filterParameters.FilterConditions = DeserializeFromJson<FilterCondition>(conditions);
            }

            IList<TModel> results = this.genericService.Select(filterParameters);
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
                baseDateTime = baseDateTime.AddHours(double.Parse(match.Groups[3].Value) / 100);
            }
            else
            {
                baseDateTime = baseDateTime.AddHours(-double.Parse(match.Groups[3].Value) / 100);
            }
            return baseDateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        //
        // GET: /GenericArea/Test/Details/5
        virtual public ActionResult Details(Int32 id)
        {
            return Details((Object)id);
        }

        private ActionResult Details(Object id)
        {
            TModel model = this.genericService.SelectById(id);

            TViewModel viewModel = Activator.CreateInstance<TViewModel>();
            viewModel.Instance = model;

            PopulateDropDowns(viewModel);

            return FormatResult(viewModel);
        }

        protected void PopulateDropDowns(TViewModel viewModel)
        {
            TModel model = viewModel.Instance;

            Type modelType = typeof(TModel);

            List<SelectList> lists = new List<SelectList>();
            foreach (PropertyInfo pInfo in modelType.GetProperties())
            {
                if (pInfo.PropertyType.Namespace == modelType.Namespace)
                {
                    Type relatedEntityType = pInfo.PropertyType;

                    String modelTypeName = relatedEntityType.Name;
                    String modelFullTypeName = relatedEntityType.FullName;

                    String displayPropertyName = null;

                    IEnumerable selectResult = genericService.SelectByType(relatedEntityType);

                    Object[] attributes = pInfo.PropertyType.GetCustomAttributes(typeof(DisplayColumnAttribute), false);
                    if (attributes.Length == 1)
                    {
                        DisplayColumnAttribute displayColumn = (DisplayColumnAttribute)attributes[0];
                        displayPropertyName = displayColumn.DisplayColumn;
                    }
                    else
                    {
                        PropertyInfo stringProperty = pInfo.PropertyType.GetProperties().FirstOrDefault(x => x.PropertyType == typeof(String));
                        if (stringProperty != null) displayPropertyName = stringProperty.Name;
                    }

                    Object selValue = null;
                    if (model != null)
                    {
                        Object obj = pInfo.GetValue(model, null);
                        if (obj != null)
                        {
                            var propertyInfo = obj.GetType().GetProperty("ID");

                            if (propertyInfo == null)
                            {
                                if (obj.GetType().IsEnum)
                                {
                                    selValue = obj.ToString();
                                }
                                else
                                {
                                    throw new Exception(String.Format("Não existe a propriedade {0} na entidade {1}", "ID", obj.GetType()));
                                }
                            }
                            else
                            {
                                selValue = propertyInfo.GetValue(obj, null);
                            }
                        }
                    }

                    viewModel.SelectLists[modelTypeName] = new SelectList((IEnumerable)selectResult, "ID", displayPropertyName, selValue);
                }
            }
        }

        //
        // GET: /GenericArea/Test/Create
        virtual public ActionResult Create()
        {
            TViewModel viewModel = (TViewModel)Activator.CreateInstance(typeof(TViewModel));
            viewModel.Instance = (TModel)Activator.CreateInstance(typeof(TModel));
            PopulateDropDowns(viewModel);
            return View(viewModel);
        }

        //
        // POST: /GenericArea/Test/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        virtual public ActionResult Create(TViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.genericService.Insert(viewModel.Instance);

                return RedirectToAction("Index");
            }

            PopulateDropDowns(viewModel);
            return View(viewModel);
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
        [ValidateAntiForgeryToken]
        virtual public ActionResult Delete(TModel obj)
        {
            try
            {
                this.genericService.Delete(obj);
            }
            catch
            {
                ViewData["EntityHasRelationshipError"] = true;

                TViewModel viewModel = Activator.CreateInstance<TViewModel>();
                viewModel.Instance = obj;

                PopulateDropDowns(viewModel);

                return FormatResult(viewModel);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /GenericArea/Test/Edit/5
        virtual public ActionResult Edit(Int32 id)
        {
            return Edit((Object)id);
        }

        virtual protected ActionResult Edit(String key)
        {
            return Edit((Object)key);
        }

        virtual protected ActionResult Edit(Object key)
        {
            return Details(key);
        }

        //
        // POST: /GenericArea/Test/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        virtual public ActionResult Edit(TViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.genericService.Update(viewModel.Instance);
                return RedirectToAction("Index");
            }
            PopulateDropDowns(viewModel);
            return View(viewModel);
        }

        // GET: /GenericArea/Test/EditModal/5
        virtual public ActionResult EditModal(int id)
        {
            TModel obj = this.genericService.SelectById(id);
            return View("Edit", "FormModal", obj);
        }

        // Post: Upload File
        public string UploadFile(HttpPostedFileBase FileData)
        {
            String entityName = typeof(TModel).Name;
            string fileDir = Request.MapPath("~/Files/" + entityName + "/");
            string fileName = DateTime.Now.ToString("ddMMyyhhss") + FileData.FileName;
            if (!System.IO.Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            FileData.SaveAs(fileDir + fileName);

            return "~/Files/" + entityName + "/" + fileName;
        }
    }
}