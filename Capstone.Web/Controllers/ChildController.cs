using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class ChildController : Controller
    {
        public ActionResult Login()
        {
            return View("Login");
        }
    }
}