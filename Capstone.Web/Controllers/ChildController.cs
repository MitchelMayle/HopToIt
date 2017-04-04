using Capstone.Web.DAL.Child;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class ChildController : Controller
    {
        private readonly IChildDAL dal;
        public ChildController (IChildDAL dal)
        {
            this.dal = dal;
        }

        public ActionResult Login()
        {
            return View("Login");
        }
    }
}