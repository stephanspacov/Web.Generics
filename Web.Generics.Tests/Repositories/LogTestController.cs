using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Web.Generics.Infrastructure.Logging;

namespace Web.Generics.Tests.Repositories
{
    public class LogTestController : Controller
    {
        [Loggable]
        public ActionResult DoSomethingWithLog()
        {
            return Content("DoSomethingWithLog");
        }
    }
}
