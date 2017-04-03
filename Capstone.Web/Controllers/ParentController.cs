using Capstone.Web.DAL.Parent;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Capstone.Web.Controllers
{

    public class ParentController : Controller
    {
        private readonly IParentDAL dal;
        public ParentController(IParentDAL dal)
        {
            this.dal = dal;
        }
        public ActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(ParentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            ParentModel parent = dal.GetParent(model);

            if (parent == null || parent.Password != model.Password)
            {
                ModelState.AddModelError("invalid-credentials", "Invalid email password combination");
                return View("Login", model);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(parent.Email, true);
                Session[SessionKeys.ParentId] = parent.Parent_ID;

            }


            return RedirectToAction("Dashboard", "Parent", model);
        }
        public ActionResult Registration()
        {
            return View("Registration");
        }
        [HttpPost]
        public ActionResult Registration(ParentModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("Registration", model);
            }

            ParentModel parent = dal.GetParent(model);
            if(parent != null)
            {
                ModelState.AddModelError("email-exists", "That email address is already registered");
                return View("Registration", model);             
            }
            else
            {

            }
            dal.CreateParent(model);

            return RedirectToAction("Dashboard", "Parent", model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove(SessionKeys.ParentId);

            return RedirectToAction("Index", "Home");
        }

    }
}