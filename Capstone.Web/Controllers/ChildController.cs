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
        public ChildController(IChildDAL dal)
        {
            this.dal = dal;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            if (Session["parent"] == null)
            {
                return RedirectToAction("Login", "Parent", null);
            }
            return View("Registration");
        }

        [HttpPost]
        public ActionResult Registration(ChildRegistrationModel viewModel)
        {

            if(Session["parent"] == null)
            {
                return RedirectToAction("Login", "Parent", null);
            }
            if (!ModelState.IsValid)
            {
                return View("Registration", viewModel);
            }

            ChildModel child = dal.GetChild(viewModel.User_Name);

            if (child != null)
            {
                ModelState.AddModelError("invalid-credentials", "Invalid username and password combination");
                return View("Registration", viewModel);
            }
            else
            {
                
                child = new ChildModel
                {
                    First_Name = viewModel.First_Name,
                    UserName = viewModel.User_Name,

                };

                HashProvider hash = new HashProvider();
                child.Password = hash.HashPassword(viewModel.Password);
                child.Salt = hash.SaltValue;
                ParentModel parent = Session["parent"] as ParentModel;
                child.Parent_Id = parent.Parent_ID;

                dal.CreateChild(child);
            }
            Session["child"] = child;
            return RedirectToAction("Dashboard", "Parent");
        }

        [HttpPost]
        [Route("ChildLogin")]
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

            Session["child"] = child;
            return RedirectToAction("Dashboard", "Child", child);
        }

        [Route("ChildDashboard")]
        public ActionResult Dashboard()
        {

            if (Session["child"] == null)
            {
                return View("Login");
            }
            ChildModel child = Session["child"] as ChildModel;
            return View("Dashboard", child);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove(SessionKeys.ChildId);

            return RedirectToAction("Index", "Home");
        }
    }
}