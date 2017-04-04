using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        [ChildActionOnly]
        public ActionResult Navigation()
        {
            if (Session["parent"] != null)
            {
                return PartialView("_ParentAuthenticated");
            }
            if (Session["child"] != null)
            {
                return PartialView("_ChildAuthenticated");
            }

            return PartialView("_NotLoggedIn");
        }

        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }
    }
}