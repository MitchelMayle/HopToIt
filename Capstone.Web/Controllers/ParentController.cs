using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class ParentController : Controller
    {
        public ActionResult ParentLogin()
        {
            return View("ParentLogin");
        }
        public ActionResult ParentRegister()
        {
            return View("ParentRegister");
        }
    }
}