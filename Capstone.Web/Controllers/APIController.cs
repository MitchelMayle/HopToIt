using Capstone.Web.DAL.Child;
using Capstone.Web.DAL.Mascot;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Capstone.Web.Controllers
{
    public class APIController : Controller
    {
        private readonly IChildDAL childDAL;
        private readonly IMascotDAL mascotDAL;
        public APIController(IChildDAL childDAL, IMascotDAL mascotDAL)
        {
            this.childDAL = childDAL;
            this.mascotDAL = mascotDAL;
        }

        public ActionResult GetTime(string userName)
        {
            ChildModel child = childDAL.GetChild(userName);

            return Json(child.Seconds, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateTime(string userName, int secondsRemaining)
        {
            childDAL.UpdateSeconds(userName, secondsRemaining);

            if (secondsRemaining == 0)
            {
                return RedirectToAction("Logout", "Home");
            }

            return null;
        }

        public ActionResult AddCarrot(string userName)
        {
            childDAL.AddCarrot(userName);

            ChildModel child = childDAL.GetChild(userName);
            child.Mascot = mascotDAL.GetMascot(child);
            Session["child"] = child;

            return null;
        }
    }
}