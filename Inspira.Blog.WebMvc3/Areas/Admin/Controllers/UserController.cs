using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc3.Areas.Admin.Models;
using Web.Generics.UserInterface.Models;
using Web.Generics.DomainServices;

namespace Inspira.Blog.WebMvc3.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        public ActionResult List()
        {
            return View(new UserViewModel { Grid = new Grid { PagingInfo=new PagingInfo { TotalItemCount=44, PagingEnabled=true, PageSize=10, PageIndex=2 }, Columns = new [] { new GridColumn { HeaderText = "Column 1", PropertyName = "PropertyName" } }, 
                Rows=new[]{
                    new GridRow { KeyValue="08", Cells = new [] { new GridCell { Value="Cell value" } },  },
                    new GridRow { KeyValue="08", Cells = new [] { new GridCell { Value="Cell value" } },  }
                } }});
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
