using Capstone.Web.DAL.Child;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Capstone.Web.Controllers
{
    public class API_TimeController : Controller
    {
        private readonly IChildDAL childDAL;
        public API_TimeController(IChildDAL childDAL)
        {
            this.childDAL = childDAL;
        }

        [Route("api/getTime")]
        public ActionResult GetTime(string userName)
        {
            ChildModel child = childDAL.GetChild(userName);

            return Json(child.Seconds, JsonRequestBehavior.AllowGet);
        }

        [Route("api/updateTime")]
        public ActionResult UpdateTime(string userName, int secondsRemaining)
        {
            childDAL.UpdateSeconds(userName, secondsRemaining);

            if (secondsRemaining == 0)
            {
                return RedirectToAction("Logout", "Home");
            }

            return null;
        }
    }
}