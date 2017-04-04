using Capstone.Web.DAL.Parent;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Capstone.Web.Crypto;

namespace Capstone.Web.Controllers
{

    public class ParentController : Controller
    {
        private readonly IParentDAL dal;
        public ParentController(IParentDAL dal)
        {
            this.dal = dal;
        }

        [HttpGet]
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

            HashProvider hash = new HashProvider();
            string hashPassword = hash.HashPassword(model.Password);

            if (parent == null || parent.Password != hashPassword)
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

        [HttpGet]
        public ActionResult Registration()
        {
            return View("Registration");
        }

        [HttpPost]
        public ActionResult Registration(ParentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Registration", model);
            }

            ParentModel parent = dal.GetParent(model);

            if (parent != null)
            {
                ModelState.AddModelError("email-exists", "That email address is already registered.");
                return View("Registration", model);
            }
            else
            {
                HashProvider hash = new HashProvider();
                model.Password = hash.HashPassword(model.Password);

                dal.CreateParent(model);
            }
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