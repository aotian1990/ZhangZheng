using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Goceen.Website.Web.Controllers
{
    public class PartialController : Controller
    {
        // GET: Partial
        public ActionResult Footer()
        {
            return View();
        }

        public ActionResult Header()
        {
            return View();
        }

        public ActionResult AppHeader()
        {
            return View();
        }
    }
}