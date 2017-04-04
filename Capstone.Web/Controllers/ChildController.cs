using Capstone.Web.Crypto;
using Capstone.Web.DAL.Child;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Capstone.Web.Controllers
{
    public class ChildController : Controller
    {
        private readonly IChildDAL dal;
        public ChildController (IChildDAL dal)
        {
            this.dal = dal;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(ChildLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            ChildModel child = dal.GetChild(model.UserName);

            HashProvider hash = new HashProvider();

            if (child == null || !hash.VerifyPasswordMatch(child.Password, model.Password, child.Salt))
            {
                ModelState.AddModelError("invalid-credentials", "Invalid email password combination");
                return View("Login", model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(child.UserName, true);
                Session[SessionKeys.ChildId] = child.Child_Id;
            }

            return RedirectToAction("Dashboard", "Child", child);
        }

        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }

        //public ActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();
        //    Session.Remove(SessionKeys.ParentId);

        //    return RedirectToAction("Index", "Home");
        //}
    }
}