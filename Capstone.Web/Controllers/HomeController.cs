using Capstone.Web.Models;
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
                ParentModel parent = Session["parent"] as ParentModel;
                return PartialView("_ParentAuthenticated", parent);
               
            }
            if (Session["child"] != null)
            {
                ChildModel child = Session["child"] as ChildModel;
                return PartialView("_ChildAuthenticated", child);
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